using CloudService.TSP;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudService.LoginService
{
    [DataContract] 
    public class User
    {
        [DataMember]
        public List<TSPCalculation> ActiveCalculations { get; private set; }
        [DataMember]
        public List<TSPCalculation> FinishedCalculations { get; set; }

        [DataMember]
        public string UserID { get; private set; }
        [DataMember]
        public string Password { get; private set; }

        public User(string userID, string password)
        {
            UserID = userID;
            Password = password;
            ActiveCalculations = new List<TSPCalculation>();
            FinishedCalculations = new List<TSPCalculation>();
        }
    }
}
