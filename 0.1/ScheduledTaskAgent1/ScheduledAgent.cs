#define DEBUG_AGENT

using CloudService.Cloud;
using CloudService.TSP;
using Microsoft.Phone.Scheduler;
using Microsoft.Phone.Shell;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

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
            //#if DEBUG_AGENT
            //    ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(10));
            //#endif

            // Loopa igenom alla calculations i Cloud och uppdatera tile samt
            // visa toast (?) när loopen stöter på en TSPCalculation med result
            // satt till 1

            /*List<TSPCalculation> calculations = Cloud.GetCalculations(user);
            for (int i = 0; i < calculations.Count; i++)
            {
                if (calculations.ElementAt(i).Result == 1)
                {
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
                }
            }*/

            //#if DEBUG_AGENT
            //    ScheduledActionService.LaunchForTest(task.Name, TimeSpan.FromSeconds(30));
            //    System.Diagnostics.Debug.WriteLine("Periodic task is started again: " + task.Name);
            //#endif

            NotifyComplete();
        }

    }

}

