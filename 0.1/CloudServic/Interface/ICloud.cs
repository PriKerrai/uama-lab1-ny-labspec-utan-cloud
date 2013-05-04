using CloudService.TSP;
using CloudService.LoginService;
using System.Collections.Generic;

namespace CloudService.Interface
{
    interface ICloud
    {
        void AddCalculation(string userID, City[] citiesToVisit);

        //void MoveCalculationToFinished(string userID, int number);

        void NotifyClient(string userID, int number);

        bool Login(string userID, string password);

        User GetUserFromDB(string userID);

        bool CreateUser(string userID, string password);

        void StoreUser(User user);

        void LoadUserDB();

        void StoreFinishedCalculation(TSPCalculation calculation);
    }
}
