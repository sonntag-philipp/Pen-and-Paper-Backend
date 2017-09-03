using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.ViewModels.Contracts
{
    public interface IHTTPViewModel
    {
        List<IHTTPController> HttpControllerList { get; set; }

        void HandleRequest(HttpListenerContext ctx);
    }
}
