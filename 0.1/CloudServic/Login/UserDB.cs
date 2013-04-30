using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudService.LoginService
{
    [DataContract] 
    public class UserDB
    {
        private static UserDB instance;

        [DataMember]
        private List<User> users = new List<User>();

        public List<User> Users
        {
            get { return this.users; } 
        }

        private UserDB() {}

        public static UserDB Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new UserDB();
                }
                return instance;
            }
        }

        public static void LoadUserDB(UserDB userDB)
        {
            if (instance == null)
            {
                instance = userDB;
            }
        }
    }
}
