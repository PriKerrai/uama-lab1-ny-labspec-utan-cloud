using CloudService.TSP;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CloudService.Interface
{
    interface ICloud
    {
        void AddCalculation(City[] citiesToVisit);
        bool Login(string id, string pw);
        bool CreateUser(string id, string pw);
    }
}
