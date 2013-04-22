using System;
using System.Net;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Documents;
using System.Windows.Ink;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Shapes;
using System.Collections.Generic;

namespace CloudServic.Login
{
    public class LoginDB
    {
        private static LoginDB instance;

        public static LoginDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new LoginDB();
                }
                return instance;
            }
        }

        public List<User> users;
    }
}
