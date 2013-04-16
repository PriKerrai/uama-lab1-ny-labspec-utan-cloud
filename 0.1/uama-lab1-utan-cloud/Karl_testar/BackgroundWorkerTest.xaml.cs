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
using System.ComponentModel;
using Microsoft.Phone.Shell;

namespace uama_lab1_utan_cloud.Karl_testar
{
    public partial class BackgroundWorkerTest : PhoneApplicationPage
    {
        BackgroundWorker worker = new BackgroundWorker();

        public BackgroundWorkerTest()
        {
            InitializeComponent();
            setupWorker();
        }

        private void setupWorker()
        {
            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            worker.DoWork += new DoWorkEventHandler(worker_DoWork);
            worker.RunWorkerCompleted += new RunWorkerCompletedEventHandler(worker_RunWorkerCompleted);
        }

        void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            for (int x = 0; x < 5; ++x)
            {
                // simulate calculating
                System.Threading.Thread.Sleep(1000);
            }
        }

        private void startWorkButton_Click(object sender, RoutedEventArgs e)
        {
            workerStatusTextBlock.Text = "Worker started - will work for 5 seconds.";
            worker.RunWorkerAsync();

        }

        void worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (e.Error != null)
            {
                MessageBox.Show("Error occurred...");
            }
            else
            {
                MessageBox.Show("Worker finished.");

                ShellToast toast = new ShellToast();
                toast.Content = "Worker finished.";
                toast.Title = "TSP calculation";
                toast.Show();
            }
        }
    }
}