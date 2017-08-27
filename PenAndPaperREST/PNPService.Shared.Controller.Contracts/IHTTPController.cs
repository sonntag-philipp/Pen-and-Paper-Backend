using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Controller.Contracts
{
    public interface IHTTPController
    {
        HttpListener Listener { get; set; }

        void StartListener();
        void StopListener();

        void HandleRequest(HttpListenerContext context);
    }
}
