using CloudService.Cloud;
using CloudService.TSP;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Controls;
using System.IO.IsolatedStorage;
using System.IO;
using System.ComponentModel;
using CloudService.LoginService;


namespace uama_lab1_utan_cloud
{
    public partial class UserPage : PhoneApplicationPage
    {
        private IsolatedStorageFile isoFile;

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

        public void listAllFinishedCalculations(User user)
        {
            string fileName = GetUserID() + "-Calc";
            int i = 0;
            List<String> list = new List<String>();

            using (StreamReader reader = new StreamReader(new IsolatedStorageFileStream(fileName, FileMode.Open, FileAccess.Read, isoFile)))
            {
                while (i < user.NumCalculations)
                {
                    if (Cloud.Instance.IsoFile.FileExists(fileName + i + ".txt"))
                    {

                        list.Add(reader.ReadToEnd());

                    }

                    i++;
                }
                calculationsListBox.Items.Add(list);
            }

        }



    }
}
