using CloudService.Interface;
using CloudService.LoginService;
using CloudService.TSP;
using Microsoft.Phone.Shell;
using System;
using System.Linq;
using System.Runtime.Serialization;
using System.IO.IsolatedStorage;
using System.IO;
using System.Collections.Generic;

namespace CloudService.Cloud
{

    public class Cloud : ICloud
    {
        private static Cloud instance;

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

            user.Calculations.Add(calculation);
            calculation.YoloSwag(userID, user.Calculations.Count + 1);
            //calculation.Start(user, citiesToVisit);
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

        public void UpdateCalculation(User user)
        {
            /*List<User> users = LoginDB.Instance.Users;
            for (int i = 0; i < LoginDB.Instance.Users.Count; i++)
            {
                if (users.ElementAt(i).UserID.Equals(user.UserID))
                {
                    for (int j = 0; j < users.ElementAt(i).Calculations.Count; j++)
                    {
                        if (users.ElementAt(i).Calculations.ElementAt(j).Number ==
                            user.Calculations.ElementAt(j).Number &&
                            user.Calculations.ElementAt(j).Result == 1)
                        {

                        }
                    }
                }
            }*/
        }

        public User GetUserFromDB(string userID)
        {
            User error = default(User);
            for (int i = 0; i < LoginDB.Instance.Users.Count; i++)
            {
                if (LoginDB.Instance.Users.ElementAt(i).UserID.Equals(userID))
                    return LoginDB.Instance.Users.ElementAt(i);
            }
            return error;
        }

        public void StoreUser(User sourceData, string targetFileName)
        {
            DataContractSerializer serializer = new DataContractSerializer(typeof(User));
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication();
            try
            {
                using (var targetFile = isoFile.CreateFile(targetFileName + ".dat"))
                {
                    serializer.WriteObject(targetFile, sourceData);
                }
            }
            catch (Exception e)
            {
                isoFile.DeleteFile(targetFileName);
            } 
        }

        public User LoadUser(string sourceName)
        {
            sourceName = sourceName + ".dat";
            DataContractSerializer serializer = new DataContractSerializer(typeof(User));
            IsolatedStorageFile isoFile = IsolatedStorageFile.GetUserStoreForApplication();

            User retVal = default(User);
            if (isoFile.FileExists(sourceName))
                using (var sourceStream = isoFile.OpenFile(sourceName, FileMode.Open))
                {
                    retVal = (User)serializer.ReadObject(sourceStream);
                }
            return retVal; 
        }

    }

}
