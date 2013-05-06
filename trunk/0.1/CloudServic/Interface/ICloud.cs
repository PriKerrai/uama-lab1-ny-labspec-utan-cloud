using CloudService.TSP;
using CloudService.LoginService;
using System.Collections.Generic;

namespace CloudService.Interface
{
    interface ICloud
    {
        void AddCalculation(string userID, City[] citiesToVisit);

        bool Login(string userID, string password);

        bool CreateUser(string userID, string password);
    }
}
