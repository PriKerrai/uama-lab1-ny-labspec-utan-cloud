using CloudService.Interface;
using CloudService.LoginService;
using CloudService.TSP;
using Microsoft.Phone.Shell;
using System;
using System.Linq;
using System.IO.IsolatedStorage;
using System.IO;
using System.Diagnostics;

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
            TSPCalculation calculation = new TSPCalculation();

            User user = GetUserFromDB(userID);

            user.ActiveCalculations.Add(calculation);
            calculation.YoloSwag(userID, (user.ActiveCalculations.Count + user.FinishedCalculations.Count));
            //calculation.Start(user, citiesToVisit);
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

            // some random number
            Random random = new Random();
            // get application tile
            ShellTile tile = ShellTile.ActiveTiles.First();
            if (null != tile)
            {
                // creata a new data for tile
                StandardTileData data = new StandardTileData();
                // tile foreground data
                data.Title = "Title text here";
                data.BackgroundImage = new Uri("/Images/Blue.jpg", UriKind.Relative);
                data.Count = random.Next(99);
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
            User error = default(User);
            for (int i = 0; i < UserDB.Instance.Users.Count; i++)
            {
                if (UserDB.Instance.Users.ElementAt(i).UserID.Equals(userID))
                    return UserDB.Instance.Users.ElementAt(i);
            }
            return error;
        }

        public void StoreUser(User user)
        {
            using (StreamWriter streamWriter = new StreamWriter(new IsolatedStorageFileStream("UserDB.txt", FileMode.Create, FileAccess.Write, IsoFile)))
            {
                streamWriter.WriteLine(user.UserID+":"+user.Password);
                streamWriter.Close();
            }
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
                            //Debug.WriteLine("User: \"" + parts[0] + "\" : \"" + parts[1] + "\"");
                            if (parts[0] != null && parts[0].Length > 0
                                && parts[1] != null && parts[1].Length > 0)
                                UserDB.Instance.Users.Add(new User(parts[0], parts[1]));
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Debug.WriteLine("\nLoadUserDB Failed:\n"+e.StackTrace+"\n");
            }
        }

        /*public void UpdateUser(User user)
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
        }

        public void StoreUserDB(UserDB sourceData)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(UserDB));
            try
            {
                using (var targetFile = IsoFile.CreateFile("UserDB.dat"))
                {
                    serializer.WriteObject(targetFile, sourceData);
                }
            }
            catch (Exception e)
            {
                IsoFile.DeleteFile("UserDB.dat");
            }
        }

        public void LoadUserDB()
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(UserDB));

            UserDB userDB = default(UserDB);
            if (IsoFile.FileExists("UserDB.dat"))
            {
                using (var sourceStream = IsoFile.OpenFile("UserDB.dat", FileMode.Open))
                {
                    userDB = (UserDB)serializer.ReadObject(sourceStream);
                }
                UserDB.LoadUserDB(userDB);
            }
            else
            {
                Debug.WriteLine("File do not exist.");
            }
        }*/

    }

}
