using CloudService.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudService.Interface
{
    interface ICloud
    {
        void AddCalculation(string user, City[] citiesToVisit);

        List<TSPCalculation> GetCalculations(string user);

        bool Login(string id, string pw);

        bool CreateUser(string id, string pw);
    }
}
