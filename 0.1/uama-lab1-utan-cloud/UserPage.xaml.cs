using CloudService.Cloud;
using CloudService.LoginService;
using Microsoft.Phone.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Windows;
using System.Windows.Controls;


namespace uama_lab1_utan_cloud
{
    public partial class UserPage : PhoneApplicationPage
    {
        public UserPage()
        {
            InitializeComponent();
            ListAllCalculations(GetUserID());
            setUserNameTextBlock();
        }

        private void newCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewCalculation.xaml", UriKind.Relative));
        }

        private void setUserNameTextBlock()
        {
            userNameTitleTextBlock.Text = GetUserID();
        }

        private void calculationsListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (calculationsListBox.SelectedItem != null)
            {
                string[] parts = ((string)calculationsListBox.SelectedItem).
                                 Split(new char[] {' '}, StringSplitOptions.RemoveEmptyEntries);
                StoreCalculationNumber(parts[1]);
                NavigationService.Navigate(new Uri("/CalculationInfo.xaml", UriKind.Relative));
            }
        }

        public void ListAllCalculations(string userID)
        {
            string fileName = userID + "-Calc";
            int i = 1;
            List<string> list = new List<string>();

            while (i <= UserDB.Instance.GetUser(userID).NumCalculations)
            {
                if (Cloud.Instance.IsoFile.FileExists(fileName + i + ".txt"))
                {
                    using (StreamReader reader = 
                        new StreamReader(
                            new IsolatedStorageFileStream(
                                fileName + i + ".txt", FileMode.Open, FileAccess.Read, Cloud.Instance.IsoFile
                            )
                        )
                    )
                    {
                        list.Add(reader.ReadLine());
                        reader.Close();
                    }
                }
                i++;
            }
            if (list.Count > 0)
                calculationsListBox.ItemsSource = list;
        }

        private string GetUserID()
        {
            using (IsolatedStorageFileStream stream = 
                new IsolatedStorageFileStream(
                    "UserID.txt", FileMode.Open, FileAccess.Read, Cloud.Instance.IsoFile
                )
            )
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string userID = reader.ReadLine();
                    reader.Close();
                    return userID;
                }
            }
        }

        private void StoreCalculationNumber(string number)
        {
            using (IsolatedStorageFileStream stream =
                new IsolatedStorageFileStream(
                    "CalculationID.txt", FileMode.Create, FileAccess.Write, Cloud.Instance.IsoFile
                )
            )
            {
                using (StreamWriter writer = new StreamWriter(stream))
                {
                    writer.WriteLine(number);
                    writer.Close();
                }
            }
        }

    }
}
