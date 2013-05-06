using System;
using Microsoft.Phone.Controls;
using CloudService.Cloud;
using System.IO;
using System.IO.IsolatedStorage;
using System.Collections.Generic;
using System.Linq;

namespace uama_lab1_utan_cloud
{
    public partial class CalculationInfo : PhoneApplicationPage
    {
        public CalculationInfo()
        {
            InitializeComponent();
            LoadCalculationInfo();
        }

        private void LoadCalculationInfo()
        {
            string userID = GetUserID();
            int calculationID = Convert.ToInt32(GetCalculationID());
            List<string> cities = new List<string>();

            calculationTextBlock.Text = calculationTextBlock.Text + calculationID;

            using (IsolatedStorageFileStream stream =
                new IsolatedStorageFileStream(
                    userID + "-Calc" + calculationID + ".txt", FileMode.Open, FileAccess.Read, Cloud.Instance.IsoFile
                )
            )
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    // Skip the two first lines
                    reader.ReadLine(); 
                    reader.ReadLine();

                    string line = "";
                    while (!(line = reader.ReadLine()).Contains("Shortest path"))
                    {
                        cities.Add(line);
                    }
                    reader.Close();
                    resultTextBlock.Text = line + " miles";
                    inputCitiesListBox.ItemsSource = cities;
                }
            }
        }

        private string GetCalculationID()
        {
            using (IsolatedStorageFileStream stream = 
                new IsolatedStorageFileStream(
                    "CalculationID.txt", FileMode.Open, FileAccess.Read, Cloud.Instance.IsoFile
                )
            )
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string calculationID = reader.ReadLine();
                    reader.Close();
                    return calculationID;
                }
            }
        }

        private string GetUserID()
        {
            IsolatedStorageFile isoFile = Cloud.Instance.IsoFile;

            using (IsolatedStorageFileStream stream = new IsolatedStorageFileStream("UserID.txt", FileMode.Open, FileAccess.Read, isoFile))
            {
                using (StreamReader reader = new StreamReader(stream))
                {
                    string userID = reader.ReadLine();
                    reader.Close();
                    return userID;
                }
            }
        }
    }
}