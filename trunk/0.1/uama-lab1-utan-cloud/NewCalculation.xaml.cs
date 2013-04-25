#define DEBUG_AGENT

using CloudService.Cloud;
using CloudService.TSP;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Scheduler;
using ScheduledTaskAgent1;
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
    public partial class NewCalculation : PhoneApplicationPage
    {
        PeriodicTask periodicTask;

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
            int numCities = citiesToVisitListBox.Items.Count;
            string[] cityNames = new string[numCities];
            City[] citiesToVisit = new City[numCities];
            
            citiesToVisitListBox.Items.CopyTo(cityNames, 0);
            for (int i = 0; i < numCities; i++)
            {
                citiesToVisit[i] = Cities.GetCityByName(cityNames[i]);
            }

            Cloud.Instance.AddCalculation("SlimeFish", citiesToVisit);

            //StartPeriodicAgent();
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

        private void StartPeriodicAgent()
        {
            // Obtain a reference to the period task, if one exists
            periodicTask = ScheduledActionService.Find("tsp") as PeriodicTask;

            // If the task already exists and background agents are enabled for the
            // application, you must remove the task and then add it again to update 
            // the schedule
            if (periodicTask != null)
            {
                RemoveAgent("tsp");
            }

            periodicTask = new PeriodicTask("tsp");

            // The description is required for periodic agents. This is the string that the user
            // will see in the background services Settings page on the device.
            periodicTask.Description = "This demonstrates a periodic task.";

            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(periodicTask);

                // If debugging is enabled, use LaunchForTest to launch the agent in ten seconds.
                #if(DEBUG_AGENT)
                    ScheduledActionService.LaunchForTest("tsp", TimeSpan.FromSeconds(10));
                #endif
            }
            catch (InvalidOperationException exception)
            {
                if (exception.Message.Contains("BNS Error: The action is disabled"))
                {
                    MessageBox.Show("Background agents for this application have been disabled by the user.");
                }

                if (exception.Message.Contains("BNS Error: The maximum number of ScheduledActions of this type have already been added."))
                {
                    // No user action required. The system prompts the user when the hard limit of periodic tasks has been reached.
                }
            }
        }

        private void RemoveAgent(string name)
        {
            try
            {
                ScheduledActionService.Remove(name);
            }
            catch (Exception)
            {
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