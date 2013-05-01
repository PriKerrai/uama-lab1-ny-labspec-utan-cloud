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
            Cloud.Instance.LoadUserDB();
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
                if (Cloud.Instance.CreateUser(user.UserID, user.Password))
                {
                    StoreUserID();
                    IsolatedStorageSettings.ApplicationSettings["userID"] = user.UserID;

                    Cloud.Instance.StoreUser(user);

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
            if (Cloud.Instance.GetUserFromDB("SlimeFish") == null) // <DEBUG ---
            {
                Cloud.Instance.CreateUser("SlimeFish", "abcdef");
                Cloud.Instance.StoreUser(new User("SlimeFish", "abcdef"));
            } // --- DEBUG>
            NavigationService.Navigate(new Uri("/NewCalculation.xaml", UriKind.Relative));
        }

        private void StoreUserID()
        {
            using (StreamWriter streamWriter = new StreamWriter(new IsolatedStorageFileStream("UserID.txt", FileMode.Create, FileAccess.Write, Cloud.Instance.IsoFile)))
            {
                string userID = userNameTextBox.Text;
                streamWriter.WriteLine(userID);
                streamWriter.Close();
            }
        }

        private string GetUserID()
        {
            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("UserID.txt", FileMode.Open, Cloud.Instance.IsoFile))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string fileContent = reader.ReadLine();
                    return fileContent;
                }
            }
        }
    }
}