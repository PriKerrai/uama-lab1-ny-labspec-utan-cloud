using CloudService.Interface;
using CloudService.LoginService;
using CloudService.TSP;
using Microsoft.Phone.Shell;
using System;
using System.Linq;
using System.IO.IsolatedStorage;
using System.IO;
using System.Diagnostics;
using System.Collections.Generic;

namespace CloudService.Cloud
{

    public class Cloud : ICloud
    {
        private static Cloud instance;

        private IsolatedStorageFile isoFile; 
        public IsolatedStorageFile IsoFile
        {
            get
            {
                if (isoFile == null)
                    isoFile = IsolatedStorageFile.GetUserStoreForApplication();
                return isoFile;
            }
        }

        public static Cloud Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Cloud();
                }
                return instance;
            }
        }

        private Cloud() {}

        public void AddCalculation(string userID, City[] citiesToVisit)
        {
            GetUserFromDB(userID).NumCalculations++;

            TSPCalculation calculation = new TSPCalculation(userID, GetUserFromDB(userID).NumCalculations);

            GetUserFromDB(userID).ActiveCalculations.Add(calculation);
            calculation.YoloSwag();
            //calculation.Start(user, citiesToVisit);
            StoreUser(GetUserFromDB(userID));
        }

        public void MoveCalculationToFinished(string userID, int number)
        {
            User user = GetUserFromDB(userID);

            for (int i = 0; i < user.ActiveCalculations.Count; i++)
            {
                if (user.ActiveCalculations.ElementAt(i).Number == number)
                {
                    user.FinishedCalculations.Add(user.ActiveCalculations.ElementAt(i));
                    user.ActiveCalculations.RemoveAt(i);
                    break;
                }
            }
        }

        public void NotifyClient(string userID, int number)
        {
            ShellToast toast = new ShellToast();
            toast.Title = "TSP";
            toast.Content = "Calculation no. " + number + " complete!";
            toast.Show();

            // get application tile
            ShellTile tile = ShellTile.ActiveTiles.First();
            if (null != tile)
            {
                // creata a new data for tile
                StandardTileData data = new StandardTileData();
                // tile foreground data
                data.Title = "Title text here";
                data.BackgroundImage = new Uri("/Images/Blue.jpg", UriKind.Relative);
                data.Count = GetUserFromDB(userID).FinishedCalculations.Count;
                // update tile
                tile.Update(data);
            }
        }

        public bool Login(string userID, string password)
        {
            Login login = new Login();
            User user = new User(userID, password);

            return login.LoginUser(user) ? true : false;
        }

        public bool CreateUser(string userID, string password)
        {
            Login login = new Login();
            User user = new User(userID, password);

            return login.CreateUser(user) ? true : false;
        }

        public User GetUserFromDB(string userID)
        {
            for (int i = 0; i < UserDB.Instance.Users.Count; i++)
            {
                if (UserDB.Instance.Users.ElementAt(i).UserID.Equals(userID))
                    return UserDB.Instance.Users.ElementAt(i);
            }
            return null;
        }

        public void StoreUser(User user)
        {
            string[] lines = GetUsers();

            char[] delimiters = new char[] { ':' };
            for (int i = 0; i < lines.Length; i++)
            {
                string[] parts = lines[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0].Equals(user.UserID))
                {
                    parts[2] = Convert.ToString(user.NumCalculations);
                    lines[i] = parts[0] + ":" + parts[1] + ":" + parts[2];
                }
            }
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("UserDB.txt", FileMode.Create, FileAccess.Write, IsoFile))
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    for (int i = 0; i < lines.Length; i++)
                    {
                        writer.WriteLine(lines[i]);
                    }
                }
            }
        }

        private string[] GetUsers()
        {
            string line = "";
            string[] lines = new string[0];

            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("UserDB.txt", FileMode.Open, FileAccess.Read, IsoFile))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    List<string> linesList = new List<string>();
                    while ((line = reader.ReadLine()) != null)
                    {
                        linesList.Add(line);
                    }
                    lines = linesList.ToArray();
                }
            }
            return lines;
        }

        public void LoadUserDB()
        {
            string line = "";

            if (!IsoFile.FileExists("UserDB.txt"))
            {
                IsoFile.CreateFile("UserDB.txt");
            }
            try
            {
                using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("UserDB.txt", FileMode.Open, IsoFile))
                {
                    using (StreamReader reader = new StreamReader(stream))
                    {
                        char[] delimiters = new char[] { ':' };
                        while ((line = reader.ReadLine()) != null)
                        {
                            string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                            Debug.WriteLine("User: \"" + parts[0] + "\" : \"" + parts[1] + "\" : \"" + parts[2]);
                            if (parts[0] != null && parts[0].Length > 0
                                && parts[1] != null && parts[1].Length > 0)
                                UserDB.Instance.Users.Add(new User(parts[0], parts[1], Convert.ToInt32(parts[2])));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\nLoadUserDB Failed:\n"+e.StackTrace+"\n");
            }
        }

        public void StoreFinishedCalculation(TSPCalculation calculation)
        {
            // File name format: Username-Calc#.txt
            string fileName = calculation.UserID + "-Calc" + calculation.Number + ".txt";

            if (!IsoFile.FileExists(fileName))
            {
                using (StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.Create, FileAccess.Write, IsoFile)))
                {
                    writer.WriteLine("This is calculation number "+calculation.Number+", initiated by user "+calculation.UserID);
                    writer.Close();
                }
            }
        }

        /*public void UpdateUser(string userID)
        {
            for (int i = 0; i < UserDB.Instance.Users.Count; i++)
            {
                if (UserDB.Instance.Users.ElementAt(i).UserID.Equals(user.UserID))
                {
                    if (UserDB.Instance.Users.ElementAt(i).FinishedCalculations.Count > 0)
                    {
                        for (int j = 0; j < UserDB.Instance.Users.ElementAt(i).FinishedCalculations.Count; j++)
                        {
                            user.FinishedCalculations.Add(UserDB.Instance.Users.ElementAt(i).FinishedCalculations.ElementAt(j));
                        }
                        UserDB.Instance.Users.ElementAt(i).FinishedCalculations = user.FinishedCalculations;
                    }
                }
            }
        }*/

    }

}
