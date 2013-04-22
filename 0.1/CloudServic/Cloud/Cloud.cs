using CloudService.Interface;
using CloudService.TSP;
using CloudServic.Login;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudService.Cloud
{

    public class Cloud : ICloud
    {
        private static Cloud instance;

        private List<TSPCalculation> calculations = new List<TSPCalculation>();

        public static Cloud Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new Cloud();
                }
                return instance;
            }
        }

        private Cloud() {}

        public void AddCalculation(City[] citiesToVisit)
        {
            TSPCalculation calculation = new TSPCalculation();

            calculation.YoloSwag();
            //calculation.Start(citiesToVisit);
            calculations.Add(calculation);
        }

        public bool Login(string id, string pw)
        {
            Login login = new Login();
            User user = new User(id, pw);

            return login.LoginUser(user) ? true: false;
        }

        public bool CreateUser(string id, string pw)
        {
            Login login = new Login();
            User user = new User(id, pw);

            return login.CreateUser(user) ? true : false;
        }
    }

}
