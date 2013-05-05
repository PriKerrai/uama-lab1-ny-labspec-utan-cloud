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
            Debug.WriteLine("Count: " + GetUserFromDB(userID).ActiveCalculations.Count);
            calculation.YoloSwag();
            //calculation.Start(user, citiesToVisit);
            StoreUser(GetUserFromDB(userID));
        }

        public void MoveCalculationToFinished(string userID, int number)
        {
            for (int i = 0; i < GetUserFromDB(userID).ActiveCalculations.Count; i++)
            {
                if (GetUserFromDB(userID).ActiveCalculations.ElementAt(i).Number == number)
                {
                    GetUserFromDB(userID).FinishedCalculations.Add(GetUserFromDB(userID).ActiveCalculations.ElementAt(i));
                    GetUserFromDB(userID).ActiveCalculations.RemoveAt(i);
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
            Debug.WriteLine("Contains user? " + ContainsUser(lines, user.UserID) + ".");
            if (ContainsUser(lines, user.UserID))
            {
                // Update already existing user
                Debug.WriteLine("Updating already existing user ...");
                for (int i = 0; i < lines.Length; i++)
                {
                    string[] parts = lines[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                    if (parts[0].Equals(user.UserID))
                    {
                        parts[2] = Convert.ToString(user.NumCalculations);
                        lines[i] = parts[0] + ":" + parts[1] + ":" + parts[2];
                    }
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
                    if (!ContainsUser(lines, user.UserID))
                    {
                        Debug.WriteLine("Adding user to DB ...");
                        writer.WriteLine(user.UserID + ":" + user.Password + ":" + user.NumCalculations);
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

        private bool ContainsUser(string[] users, string userID)
        {
            char[] delimiters = new char[] { ':' };
            for (int i = 0; i < users.Length; i++)
            {
                string[] parts = users[i].Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                if (parts[0].Equals(userID))
                {
                    return true;
                }
            }
            return false;
        }

        public void LoadUserDB()
        {
            string line = "";

            // Remove the "!" to reset UserDB
            if (!IsoFile.FileExists("UserDB.txt"))
            {
                Debug.WriteLine("UserDB.txt does not exist, creating file ...");
                IsoFile.CreateFile("UserDB.txt");
            }
            else
            {
                try
                {
                    using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("UserDB.txt", FileMode.Open, FileAccess.Read, IsoFile))
                    {
                        using (StreamReader reader = new StreamReader(stream))
                        {
                            char[] delimiters = new char[] { ':' };
                            while ((line = reader.ReadLine()) != null)
                            {
                                string[] parts = line.Split(delimiters, StringSplitOptions.RemoveEmptyEntries);
                                Debug.WriteLine("User: \"" + parts[0] + "\" : \"" + parts[1] + "\" : \"" + parts[2] + "\"");
                                if (parts[0] != null && parts[0].Length > 0
                                    && parts[1] != null && parts[1].Length > 0)
                                    UserDB.Instance.Users.Add(new User(parts[0], parts[1], Convert.ToInt32(parts[2])));
                            }
                        }
                    }
                }
                catch (Exception e)
                {
                    Debug.WriteLine("\nLoadUserDB Failed:\n" + e.StackTrace + "\n");
                }
            }
        }

        public void StoreFinishedCalculation(TSPCalculation calculation)
        {
            // File name format: Username-Calc#.txt
            string fileName = calculation.UserID + "-Calc" + calculation.Number + ".txt";
            Debug.WriteLine("Calculation file: "+calculation.UserID + "-Calc" + calculation.Number + ".txt");

            Debug.WriteLine("Calculation file did not exist, creating file ...");
            using (StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.Create, FileAccess.Write, IsoFile)))
            {
                Debug.WriteLine("Writing to calculation file ...");
                writer.WriteLine("This is calculation number "+calculation.Number+", initiated by user "+calculation.UserID);
                writer.Close();
            }
            using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream(fileName, FileMode.Open, FileAccess.Read, IsoFile)))
            {
                Debug.WriteLine("Reading from calculation file ...");
                Debug.WriteLine("Calculation info: " + reader.ReadLine());
                reader.Close();
            }
        }

    }

}
