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

namespace CloudServic.Login
{
    public class Login
    {
        LoginDB loginDB = LoginDB.Instance;

        public bool CreateUser(User user)
        {
            if (loginDB.users.Contains(user))
            {
                return false;
            }
            else
            {
                loginDB.users.Add(user);
                return true;
            }
        }

        public bool LoginUser(User user)
        {
            return loginDB.users.Contains(user) ? true: false;
        }
    }
}
