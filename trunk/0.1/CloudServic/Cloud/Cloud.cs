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
            UserDB.Instance.GetUser(userID).NumCalculations++;

            TSPCalculation calculation = new TSPCalculation(userID, UserDB.Instance.GetUser(userID).NumCalculations);

            UserDB.Instance.GetUser(userID).ActiveCalculations.Add(calculation);
            Debug.WriteLine("Count: " + UserDB.Instance.GetUser(userID).ActiveCalculations.Count);
            calculation.YoloSwag();
            //calculation.Start(user, citiesToVisit);
            UserDB.Instance.StoreUser(UserDB.Instance.GetUser(userID));
        }

        public void MoveCalculationToFinished(string userID, int number)
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
