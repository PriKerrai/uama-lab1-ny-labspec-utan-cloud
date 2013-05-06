using CloudService.Interface;
using CloudService.LoginService;
using CloudService.TSP;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Threading;

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
            UserDB.Instance.GetUser(userID).NumCalculations++;

            TSPCalculation calculation = new TSPCalculation(citiesToVisit, userID, UserDB.Instance.GetUser(userID).NumCalculations);
            UserDB.Instance.GetUser(userID).ActiveCalculations.Add(calculation);

            StoreOngoingCalculation(calculation);

            calculation.YoloSwag();
            //calculation.Start(user, citiesToVisit);

            MoveCalculationToFinished(userID, calculation.Number);
            StoreFinishedCalculation(calculation);
            UserDB.Instance.StoreUser(UserDB.Instance.GetUser(userID));
            NotifyClient(userID, calculation.Number);
        }

        private void MoveCalculationToFinished(string userID, int number)
        {
            for (int i = 0; i < UserDB.Instance.GetUser(userID).ActiveCalculations.Count; i++)
            {
                if (UserDB.Instance.GetUser(userID).ActiveCalculations.ElementAt(i).Number == number)
                {
                    UserDB.Instance.GetUser(userID).FinishedCalculations.Add(UserDB.Instance.GetUser(userID).ActiveCalculations.ElementAt(i));
                    UserDB.Instance.GetUser(userID).ActiveCalculations.RemoveAt(i);
                    break;
                }
            }
        }

        private void NotifyClient(string userID, int number)
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
                data.Count = UserDB.Instance.GetUser(userID).FinishedCalculations.Count;
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

        private void StoreOngoingCalculation(TSPCalculation calculation)
        {
            // File name format: Username-Calc#.txt
            string fileName = calculation.UserID + "-Calc" + calculation.Number + ".txt";

            using (StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.Create, FileAccess.Write, IsoFile)))
            {
                Debug.WriteLine("Writing to calculation file ...");
                writer.WriteLine("Calculation number " + calculation.Number);
                writer.WriteLine("Parameters:");
                for (int i = 0; i < calculation.CitiesToVisit.Length; i++)
                {
                    writer.WriteLine((i+1) + " " + calculation.CitiesToVisit[i].Name);
                }
                writer.Close();
            }
            using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream(fileName, FileMode.Open, FileAccess.Read, IsoFile)))
            {
                string line = "";

                Debug.WriteLine("Reading from ongoing calculation file ...");
                while ((line = reader.ReadLine()) != null)
                {
                    Debug.WriteLine(line);
                }
                reader.Close();
            }
        }

        private void StoreFinishedCalculation(TSPCalculation calculation)
        {
            // File name format: Username-Calc#.txt
            string fileName = calculation.UserID + "-Calc" + calculation.Number + ".txt";

            using (StreamWriter writer = new StreamWriter(new IsolatedStorageFileStream(fileName, FileMode.Append, FileAccess.Write, IsoFile)))
            {
                writer.WriteLine("Result: " + calculation.Result);
                writer.Close();
            }
            using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream(fileName, FileMode.Open, FileAccess.Read, IsoFile)))
            {
                string line = "";

                Debug.WriteLine("Reading from finished calculation file ...");
                while ((line = reader.ReadLine()) != null)
                {
                    Debug.WriteLine(line);
                }
                reader.Close();
            }
        }

    }

}
