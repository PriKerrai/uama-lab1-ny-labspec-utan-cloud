using CloudService.TSP;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudService.LoginService
{
    [DataContract] 
    public class User
    {
        [DataMember]
        public List<TSPCalculation> Calculations { get; private set; }

        [DataMember]
        public string UserID { get; private set; }
        [DataMember]
        public string Password { get; private set; }

        public User(string userID, string password)
        {
            UserID = userID;
            Password = password;
            Calculations = new List<TSPCalculation>();
        }
    }
}
