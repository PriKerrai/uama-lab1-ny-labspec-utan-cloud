#define DEBUG_AGENT

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

namespace uama_lab1_utan_cloud
{
    public partial class NewCalculation : PhoneApplicationPage
    {

        public NewCalculation()
        {
            InitializeComponent();
            PopulateAllCitiesListBox();
        }

        private void PopulateAllCitiesListBox()
        {
            string [] s = Cities.CityNames;

            List<string> allCities = new List<string>(s);

            allCitiesListBox.ItemsSource = allCities;
        }

        private void createCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            // create a background worker for adding the calculation to the cloud

            BackgroundWorker bw = new BackgroundWorker();
            bw.WorkerSupportsCancellation = true;
            bw.WorkerReportsProgress = true;
            bw.DoWork += new DoWorkEventHandler(bw_DoWork);
            //bw.ProgressChanged += new ProgressChangedEventHandler(bw_ProgressChanged);
            //bw.RunWorkerCompleted += new RunWorkerCompletedEventHandler(bw_RunWorkerCompleted);
            bw.RunWorkerAsync();
        }

        private void bw_DoWork(object sender, DoWorkEventArgs e)
        {
            BackgroundWorker worker = sender as BackgroundWorker;

            Cloud.Instance.AddCalculation(GetUserID(), GetCitiesToVisit());
        }

        private City[] GetCitiesToVisit()
        {
            int numCities = citiesToVisitListBox.Items.Count;
            string[] cityNames = new string[numCities];
            City[] citiesToVisit = new City[numCities];

            citiesToVisitListBox.Items.CopyTo(cityNames, 0);
            for (int i = 0; i < numCities; i++)
            {
                citiesToVisit[i] = Cities.GetCityByName(cityNames[i]);
            }

            return citiesToVisit;
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

        private void allCitiesListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != allCitiesListBox.SelectedItem)
            {
                if (citiesToVisitListBox.Items.Contains((string)allCitiesListBox.SelectedItem))
                {
                     MessageBox.Show("Visiting a city twice is not allowed.");
                }
                else
                {
                    string cityName = (string)allCitiesListBox.SelectedItem;

                    citiesToVisitListBox.Items.Add(cityName);
                }

            }
        }

        // Remove the selected city from listbox.
        private void citiesToVisitListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != citiesToVisitListBox.SelectedItem)
            {
                citiesToVisitListBox.Items.Remove(citiesToVisitListBox.SelectedItem);
            }
        }
    }
}