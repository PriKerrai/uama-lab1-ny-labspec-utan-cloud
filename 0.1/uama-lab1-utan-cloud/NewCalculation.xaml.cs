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
using Microsoft.Phone.Controls;
using ScheduledTaskAgent1;
using ScheduledTaskAgent1.TSP;
using Microsoft.Phone.Scheduler;

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
            Cities cities = new Cities();
            string[] s = cities.CityNames;
        }

        private void PopulateAllCitiesListBox()
        {
            Cities cities = new Cities();
            string [] s = cities.CityNames;

            List<string> allCities = new List<string>(s);

            allCitiesListBox.ItemsSource = allCities;
        }

        private void createCalculationButton_Click(object sender, RoutedEventArgs e)
        {
            string[] citiesToVisit = new string[citiesToVisitListBox.Items.Count];

            citiesToVisitListBox.Items.CopyTo(citiesToVisit, 0);

            TSPScheduledAgent tspAgent = new TSPScheduledAgent();


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