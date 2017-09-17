using REST.Shared.Handler.Contracts;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace REST.Shared.Listener.Contracts
{
    public interface IHttpListener
    {
        HttpListener Listener { get; }
        Thread ListenerThread { get; }
        List<IRouteHandler> Routes { get; }

        void Start();
        void Stop();

        void AddRoute(IRouteHandler route);
        void RemoveRoute(IRouteHandler route);
    }
}
