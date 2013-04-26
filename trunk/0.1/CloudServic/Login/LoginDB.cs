using System.Collections.Generic;

namespace CloudService.LoginService
{
    public class LoginDB
    {
        private static LoginDB instance;

        private List<User> users = new List<User>();

        public List<User> Users
        {
            get { return this.users; } 
        }

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
    }
}
