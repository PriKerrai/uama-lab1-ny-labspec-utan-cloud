#define DEBUG_AGENT

using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Windows;
using System.Linq;

namespace ScheduledTaskAgent1
{
    public class ScheduledAgent : ScheduledTaskAgent
    {
        private static volatile bool _classInitialized;
        private string[] citiesToVisit;

        /// <remarks>
        /// ScheduledAgent constructor, initializes the UnhandledException handler
        /// </remarks>
        public ScheduledAgent()
        {
            if (!_classInitialized)
            {
                _classInitialized = true;
                // Subscribe to the managed exception handler
                Deployment.Current.Dispatcher.BeginInvoke(delegate
                {
                    Application.Current.UnhandledException += ScheduledAgent_UnhandledException;
                });
            }
        }

        /// Code to execute on Unhandled Exceptions
        private void ScheduledAgent_UnhandledException(object sender, ApplicationUnhandledExceptionEventArgs e)
        {
            if (System.Diagnostics.Debugger.IsAttached)
            {
                // An unhandled exception has occurred; break into the debugger
                System.Diagnostics.Debugger.Break();
            }
        }

        /// <summary>
        /// Agent that runs a scheduled task
        /// </summary>
        /// <param name="task">
        /// The invoked task
        /// </param>
        /// <remarks>
        /// This method is called when a periodic or resource intensive task is invoked
        /// </remarks>
        protected override void OnInvoke(ScheduledTask task)
        {
            //TODO: Add code to perform your task in background

            // Loopa igenom alla calculations i Cloud och uppdatera tile samt
            // visa toast (?) när loopen stöter på en TSPCalculation med result
            // satt till 1

            // If debugging is enabled, launch the agent again in one minute.
#if DEBUG_AGENT
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(10));
#endif

            // Launch a toast to show that the agent is running.
            // The toast will not be shown if the foreground application is running.
            ShellToast toast = new ShellToast();
            toast.Title = "TSP";
            toast.Content = "Calculation complete.";
            toast.Show();

            // some random number
            Random random = new Random();
            // get application tile
            ShellTile tile = ShellTile.ActiveTiles.First();
            if (null != tile)
            {
                // creata a new data for tile
                StandardTileData data = new StandardTileData();
                // tile foreground data
                data.Title = "Title text here";
                data.BackgroundImage = new Uri("/Images/Blue.jpg", UriKind.Relative);
                data.Count = random.Next(99);
                // update tile
                tile.Update(data);
            }
#if DEBUG_AGENT
            ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(30));
            System.Diagnostics.Debug.WriteLine("Periodic task is started again: " + task.Name);
#endif

            NotifyComplete();



            NotifyComplete();
        }

        
    }
}

