using CloudService.Interface;
using CloudService.LoginService;
using CloudService.TSP;
using System.Collections.Generic;
using System.Linq;

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

        public void AddCalculation(string user, City[] citiesToVisit)
        {
            TSPCalculation calculation = new TSPCalculation();

            calculation.YoloSwag(user);
            //calculation.Start(user, citiesToVisit);
            //calculations.Add(calculation);
        }

        public List<TSPCalculation> GetCalculations(string user)
        {
            List<TSPCalculation> tmp = new List<TSPCalculation>();
            
            for (int i = 0; i < calculations.Count; i++)
            {
                if (calculations.ElementAt(i).User.Equals(user))
                    tmp.Add(calculations.ElementAt(i));
            }

            return tmp;
        }

        public bool Login(string id, string pw)
        {
            Login login = new Login();
            User user = new User(id, pw);

            return login.LoginUser(user) ? true : false;
        }

        public bool CreateUser(string id, string pw)
        {
            Login login = new Login();
            User user = new User(id, pw);

            return login.CreateUser(user) ? true : false;
        }

    }

}
