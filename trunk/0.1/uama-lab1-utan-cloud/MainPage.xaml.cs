using CloudService.Cloud;
using CloudService.TSP;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.IO;
using System.IO.IsolatedStorage;
using CloudService.LoginService;
using System.Diagnostics;

namespace uama_lab1_utan_cloud
{
    public partial class MainPage : PhoneApplicationPage
    {

        public MainPage()
        {
            InitializeComponent();
            Cities.InitArrays();
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            User user = getUserFromForm();

            if (user == null)
            {
                MessageBox.Show("Enter your user name and password");
            }
            else
            {
                bool validUser = Cloud.Instance.Login(user.UserID, user.Password);

                if (validUser)
                {
                    StoreUserID();
                    IsolatedStorageSettings.ApplicationSettings["userID"] = user.UserID;

                    NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("User name or password incorrect.");
                }
            }
        }

        private void newUserButton_Click(object sender, RoutedEventArgs e)
        {
            User user = getUserFromForm();

            if (user == null)
            {
                MessageBox.Show("Enter your preferred user name and password.");
            }
            else
            {
                bool userCreated = Cloud.Instance.CreateUser(user.UserID, user.Password);

                if (userCreated)
                {
                    StoreUserID();
                    IsolatedStorageSettings.ApplicationSettings["userID"] = user.UserID;

                    NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.Relative));
                }
                else
                {
                    MessageBox.Show("User name is already in use, please choose another.");
                }
            } 
        }

        private User getUserFromForm()
        {
            if (userNameTextBox.Text.Trim().Equals("") || passwordTextBox.Text.Trim().Equals(""))
            {
                return null;
            }
            else
            {
                string userID = userNameTextBox.Text.Trim();
                string password = passwordTextBox.Text.Trim();

                User user = new User(userID, password);

                return user;
            }
        }

        private void CalculationTestButton_Click(object sender, RoutedEventArgs e)
        {
            Cloud.Instance.LoadUserDB();
            // <DEBUG---
            Cloud.Instance.CreateUser("SlimeFish", "abcdef");
            Cloud.Instance.StoreUser(Cloud.Instance.GetUserFromDB("SlimeFish"), "SlimeFish");
            IsolatedStorageSettings.ApplicationSettings["userID"] = "SlimeFish";
            using (StreamWriter streamWriter = new StreamWriter(new IsolatedStorageFileStream("User.txt", FileMode.Create, FileAccess.Write, Cloud.Instance.IsoFile)))
            {
                streamWriter.WriteLine(IsolatedStorageSettings.ApplicationSettings["userID"]);
                streamWriter.Close();
            }
            User asdf = Cloud.Instance.LoadUser(GetUserID());
            Debug.WriteLine("User Object: " + asdf);
            Debug.WriteLine("User Object ID: " + asdf.UserID);
            // ---DEBUG>
            Cloud.Instance.UpdateUser(Cloud.Instance.LoadUser(GetUserID()));
            Cloud.Instance.StoreUserDB(CloudService.LoginService.UserDB.Instance); // Ta bort efter att den lyckats spara databasen en gång!
            NavigationService.Navigate(new Uri("/NewCalculation.xaml", UriKind.Relative));
        }

        private void StoreUserID()
        {
            using (StreamWriter streamWriter = new StreamWriter(new IsolatedStorageFileStream("User.txt", FileMode.Create, FileAccess.Write, Cloud.Instance.IsoFile)))
            {
                string userID = userNameTextBox.Text;
                streamWriter.WriteLine(userID);
                streamWriter.Close();
            }
        }

        private string GetUserID()
        {
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("User.txt", FileMode.Open, Cloud.Instance.IsoFile))
            {
                using (StreamReader Reader = new StreamReader(stream))
                {
                    string fileContent = Reader.ReadLine();
                    return fileContent;
                }
            }
        }
    }
}