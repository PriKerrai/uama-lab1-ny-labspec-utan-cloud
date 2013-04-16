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
using Microsoft.Phone.Scheduler;

namespace uama_lab1_utan_cloud
{
    public partial class TSPCalculation : PhoneApplicationPage
    {
        PeriodicTask periodicTask;
        ResourceIntensiveTask resourceIntensiveTask;

        public TSPCalculation()
        {
            InitializeComponent();
        }

        private void StartResourceIntensiveAgent()
        {
            //resourceIntensiveTask = new ResourceIntensiveTask("TaskName");
            //resourceIntensiveTask.Description = "Description of task.";
            periodicTask = new PeriodicTask("MyPeriodicTask");
            periodicTask.Description = "Description of my periodic task";
            
            // Place the call to Add in a try block in case the user has disabled agents.
            try
            {
                ScheduledActionService.Add(periodicTask);
            }
            catch (InvalidOperationException exception)
            {
                MessageBox.Show(exception.ToString());
            }
            catch (SchedulerServiceException)
            {
            }

            taskStatusTextBlock.Text = "Task started.";
        }

        private void startTaskButton_Click(object sender, RoutedEventArgs e)
        {
            taskStatusTextBlock.Text = "Starting task...";
            StartResourceIntensiveAgent();
            if (ScheduledActionService.Find("TaskName") != null);
            {

            }
        }
    }
}