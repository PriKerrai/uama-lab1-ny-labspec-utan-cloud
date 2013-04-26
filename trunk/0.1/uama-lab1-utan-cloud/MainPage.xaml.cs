using CloudService.Cloud;
using CloudService.TSP;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.IO;
using System.IO.IsolatedStorage;
using CloudService.LoginService;

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

        private void StoreUserID()
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            using (StreamWriter streamWriter = new StreamWriter(new IsolatedStorageFileStream("User.txt", FileMode.Create, FileAccess.Write, isolatedStorageFile)))
            {
                string userId = userNameTextBox.Text;
                streamWriter.WriteLine(userId);
                streamWriter.Close();
            }
        }

        private void ScheduledAgentTestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewCalculation.xaml", UriKind.Relative));
        }

        private void TSPCalculationPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Karl_testar/ScheduledAgentTest.xaml", UriKind.Relative));
        }

        private void backgroundWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Karl_testar/BackgroundWorkerTest.xaml", UriKind.Relative));
        }
    }
}