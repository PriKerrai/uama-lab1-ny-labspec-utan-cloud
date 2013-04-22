﻿using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;

namespace CloudServic.Login
{
    public class User
    {
        public string id { get; set; }
        public string pw { get; set; }

        public User(string id, string pw)
        {
            this.id = id;
            this.pw = pw;
        }
    }
}