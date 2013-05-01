using CloudService.TSP;
using System.Collections.Generic;
using System.Runtime.Serialization;

namespace CloudService.LoginService
{
    public class User
    {
        public List<TSPCalculation> ActiveCalculations { get; private set; }
        public List<TSPCalculation> FinishedCalculations { get; set; }

        public string UserID { get; private set; }
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
