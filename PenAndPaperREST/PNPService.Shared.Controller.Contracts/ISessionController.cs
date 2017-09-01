using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Controller.Contracts
{
    public interface ISessionController
    {

        string GenerateSessionID(string username, string password);
        void RemoveSessionID(string id);

        bool CheckSessionID(string id);
    }
}
