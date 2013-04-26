using CloudService.TSP;
using System.Collections.Generic;

namespace CloudService.Interface
{
    interface ICloud
    {
        void AddCalculation(string user, City[] citiesToVisit);

        List<TSPCalculation> GetCalculations(string user);

        bool Login(string id, string pw);

        bool CreateUser(string id, string pw);

        void NotifyClient(string user, int number);
    }
}
