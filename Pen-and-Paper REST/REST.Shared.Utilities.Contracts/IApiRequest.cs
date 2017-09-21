using System.Collections.Generic;
using System.Net;

namespace REST.Shared.Utilities.Contracts
{
    public interface IApiRequest
    {
        string[] RequestedResources { get; }
        HttpListenerContext Context { get; }
        string Route { get; }
        string URL { get; }
        string RequestedResource { get; }
        string RequestedTable { get; }
        string Method { get; }
        string Content { get; }

        void Respond(string body);
        void Respond(string body, int status);
        void Respond(int status);
        void Respond(int status, KeyValuePair<string, string>[] headers);
        void Close();
    }
}
