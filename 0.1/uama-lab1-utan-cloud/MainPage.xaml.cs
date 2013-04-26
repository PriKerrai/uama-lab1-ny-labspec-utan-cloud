using CloudService.Cloud;
using CloudService.TSP;
using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.IO;
using System.IO.IsolatedStorage;

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
            if (Cloud.Instance.Login(userNameTextBox.Text, passwordTextBox.Text))
            {
                StoreUserID();
                IsolatedStorageSettings.ApplicationSettings["userID"] = userNameTextBox.Text;
            }
            
            NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.Relative));
        }

        private void newUserButton_Click(object sender, RoutedEventArgs e)
        {
            if (Cloud.Instance.CreateUser(userNameTextBox.Text, passwordTextBox.Text))
            {
                StoreUserID();
                IsolatedStorageSettings.ApplicationSettings["userID"] = userNameTextBox.Text;
            }

            NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.Relative));
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