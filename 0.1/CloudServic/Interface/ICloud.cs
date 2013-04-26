using CloudService.TSP;
using CloudService.LoginService;
using System.Collections.Generic;

namespace CloudService.Interface
{
    interface ICloud
    {
        void AddCalculation(string user, City[] citiesToVisit);

        //List<TSPCalculation> GetCalculations(string user);

        void NotifyClient(string userID, int number);

        bool Login(string userID, string password);

        bool CreateUser(string userID, string password);

        void UpdateCalculation(User user);

        User GetUserFromDB(string userID);

        void StoreUser(User sourceData, string targetFileName);

        User LoadUser(string sourceName);
    }
}
