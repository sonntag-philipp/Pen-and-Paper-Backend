using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Handler.Contracts
{
    public interface ILoginRequestHandler
    {
        string RequestVerification(string username, string password);
        bool RequestVerification(string sessionID);

        void RequestLogout(string SessionID);
    }
}
