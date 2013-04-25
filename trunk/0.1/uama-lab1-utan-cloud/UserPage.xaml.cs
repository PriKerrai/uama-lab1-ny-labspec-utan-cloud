using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
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