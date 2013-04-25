﻿using CloudService.TSP;
using Microsoft.Phone.Controls;
using Microsoft.Phone.Notification;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.IO;
using System.IO.IsolatedStorage;

namespace uama_lab1_utan_cloud
{
    public partial class MainPage : PhoneApplicationPage
    {

        private HttpNotificationChannel channel;
        private const string channelName = "TSPCloudChannel";

        public MainPage()
        {
            InitializeComponent();
            SetupNotificationChannel();
            Cities.InitArrays();
        }

        private void SetupNotificationChannel()
        {
            channel = HttpNotificationChannel.Find(channelName);

            if (channel == null)
            {
                channel = new HttpNotificationChannel(channelName);
                channel.ChannelUriUpdated += new EventHandler<NotificationChannelUriEventArgs>(channel_ChannelUriUpdated);
                channel.Open();
            }
            else
            {
                RegisterForNotifications();
            }
        }

        private void channel_ChannelUriUpdated(object sender, NotificationChannelUriEventArgs e)
        {
            channel = HttpNotificationChannel.Find(channelName);
            channel.BindToShellToast();
            RegisterForNotifications();
        }

        private void RegisterForNotifications()
        {
            ServiceReference1.Service1Client svc = new ServiceReference1.Service1Client();
            svc.SubscribeAsync(channel.ChannelUri.ToString());

            channel.ShellToastNotificationReceived += (s, e) => Deployment.Current.Dispatcher.BeginInvoke(() => ToastReceived(e));
        }

        private void ToastReceived(NotificationEventArgs e)
        {
            NotifText.Text = string.Format("Title: " + e.Collection["wp:Text1"] + "\nMessage: " + e.Collection["wp:Text2"]);
        }

        private void logInButton_Click(object sender, RoutedEventArgs e)
        {
            storeUserId();
            
            NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.Relative));
        }

        private void newUserButton_Click(object sender, RoutedEventArgs e)
        {
            storeUserId();

            NavigationService.Navigate(new Uri("/UserPage.xaml", UriKind.Relative));
        }

        private void storeUserId()
        {
            IsolatedStorageFile isolatedStorageFile = IsolatedStorageFile.GetUserStoreForApplication();
            using (StreamWriter streamWrite = new StreamWriter(new IsolatedStorageFileStream("User.txt", FileMode.Create, FileAccess.Write, isolatedStorageFile)))
            {
                string userId = userNameTextBox.Text;
                streamWrite.WriteLine(userId);
                streamWrite.Close();
            }
        }

        private void ScheduledAgentTestButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/NewCalculation.xaml", UriKind.Relative));
        }

        private void TSPCalculationPageButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Karl_testar/ScheduledAgentTest.xaml", UriKind.Relative));
        }

        private void backgroundWorkerButton_Click(object sender, RoutedEventArgs e)
        {
            NavigationService.Navigate(new Uri("/Karl_testar/BackgroundWorkerTest.xaml", UriKind.Relative));
        }
    }
}