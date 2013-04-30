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

namespace uama_lab1_utan_cloud
{
    public partial class NewCalculation : PhoneApplicationPage
    {

        public NewCalculation()
        {
            InitializeComponent();
            PopulateAllCitiesListBox();
        }

        // usage?
        public void initCities()
        {
            string[] s = Cities.CityNames;
        }

        private void PopulateAllCitiesListBox()
        {
            string [] s = Cities.CityNames;

            List<string> allCities = new List<string>(s);

            allCitiesListBox.ItemsSource = allCities;
        }

        private void createCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            string userID = "";
            int numCities = citiesToVisitListBox.Items.Count;
            string[] cityNames = new string[numCities];
            City[] citiesToVisit = new City[numCities];
            
            citiesToVisitListBox.Items.CopyTo(cityNames, 0);
            for (int i = 0; i < numCities; i++)
            {
                citiesToVisit[i] = Cities.GetCityByName(cityNames[i]);
            }

            try
            {
                userID = (string)IsolatedStorageSettings.ApplicationSettings["userID"];
            }
            catch
            {
                IsolatedStorageSettings.ApplicationSettings.Add("userID", "");
            }
            Cloud.Instance.AddCalculation("SlimeFish", citiesToVisit); // DEBUG
            //Cloud.Instance.AddCalculation(userID, citiesToVisit);
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

        // Remove the city selected.
        private void citiesToVisitListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (null != citiesToVisitListBox.SelectedItem)
            {
                citiesToVisitListBox.Items.Remove(citiesToVisitListBox.SelectedItem);
            }
        }
    }
}