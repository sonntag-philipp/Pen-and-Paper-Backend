using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.ViewModels.Contracts
{
    public interface ISessionViewModel
    {
        IMySQLController DBController { get; set; }

        string AddSessionID(string username, string password);
        bool CheckSessionID(string sid, string username);
    }
}
