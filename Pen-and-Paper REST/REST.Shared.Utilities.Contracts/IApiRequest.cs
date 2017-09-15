using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace REST.Shared.Utilities.Contracts
{
    public interface IApiRequest
    {
        string URL { get; set; }
        string RequestedResource { get; set; }
        string RequestedTable { get; set; }
        string Method { get; set; }
        string Content { get; }

        void SendResponse(string response);
        void SendResponse(int status);
        void SendResponse(string response, int status);
        void SendError();

        void Close();

        HttpListenerContext Context { get; }
    }
}
