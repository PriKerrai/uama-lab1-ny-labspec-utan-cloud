using Microsoft.Phone.Controls;
using System;
using System.Windows;
using System.IO.IsolatedStorage;
using System.IO;

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
            userNameTextBlock.Text = getUserId();
        }

        private string getUserId()
        {
            IsolatedStorageFile Store = IsolatedStorageFile.GetUserStoreForApplication();

            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("User.txt", FileMode.Open, Store))
            {
                using (StreamReader Reader = new StreamReader(stream))
                {
                    string fileContent = Reader.ReadToEnd();
                    return fileContent;
                }
            }
        }
    }
}