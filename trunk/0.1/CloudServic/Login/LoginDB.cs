using System.Collections.Generic;

namespace CloudService.LoginService
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
