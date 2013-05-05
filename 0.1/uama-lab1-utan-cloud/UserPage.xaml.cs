using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.IO.IsolatedStorage;
using System.IO;
using CloudService.Cloud;

namespace uama_lab1_utan_cloud
{
    public partial class UserPage : PhoneApplicationPage
    {
        public UserPage()
        {
            InitializeComponent();

            setUserNameTextBlock();
        }

        private void newCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewCalculation.xaml", UriKind.Relative));
        }

        private void setUserNameTextBlock()
        {
            userNameTextBlock.Text = GetUserID();
        }

        private string GetUserID()
        {
            IsolatedStorageFile isoFile = Cloud.Instance.IsoFile;

            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("UserID.txt", FileMode.Open, FileAccess.Read, isoFile))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string userID = reader.ReadToEnd();
                    reader.Close();
                    return userID;
                }
            }
        }
    }
}