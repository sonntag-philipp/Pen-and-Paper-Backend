using REST.Shared.Handler.Contracts;
using REST.Shared.Listener.Contracts;
using REST.Shared.Utilities;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;

namespace REST.Shared.Listener
{
    public class HttpListener : IHttpListener
    {
        /// <summary>
        /// List that contains the active routes.
        /// </summary>
        public List<IRouteHandler> Routes {
            get { return _Routes; }
            private set { _Routes = value; }
        }
        private List<IRouteHandler> _Routes;

        /// <summary>
        /// Thread which contains the http listener.
        /// </summary>
        public Thread ListenerThread {
            get { return _ListenerThread; }
            private set { _ListenerThread = value; }
        }
        private Thread _ListenerThread;

        /// <summary>
        /// The HttpListener which gets the context.
        /// </summary>
        public System.Net.HttpListener Listener {
            get { return _Listener; }
            private set { _Listener = value; }
        }
        private System.Net.HttpListener _Listener;

        /// <summary>
        /// Http Listener with listener thread and route handling.
        /// </summary>
        /// <param name="prefix">The prefix of the http listener</param>
        public HttpListener(string prefix)
        {
            Routes = new List<IRouteHandler>();
            Listener = new System.Net.HttpListener();
            Listener.Prefixes.Add(prefix);

            ListenerThread = new Thread(() => ListenThread());
        }

        /// <summary>
        /// Starts the listener and the thread.
        /// </summary>
        public void Start()
        {
            Listener.Start();
            ListenerThread.Start();
        }

        /// <summary>
        /// Stops the listener.
        /// </summary>
        public void Stop()
        {
            Listener.Stop();
        }

        private void ListenThread()
        {
            while (Listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = Listener.GetContext();

                    ThreadPool.QueueUserWorkItem(c => HandleContext(context));
                }
                catch (Exception e)
                {
                    Console.WriteLine("[" + DateTime.Now + "][ERROR] Exception in listener Thread.");
                    Console.WriteLine(e.StackTrace);
                }
            }
        }

        private void HandleContext(HttpListenerContext context)
        {
            ApiRequest request = new ApiRequest(context);

            Console.WriteLine("[" + DateTime.Now + "][" + request.Method + "] " + request.URL);

            bool found = false;

            foreach (IRouteHandler item in Routes)
            {
                if(item.Route == request.Route)
                {
                    try
                    {
                        switch (context.Request.HttpMethod)
                        {
                            case "PUT":
                                item.Put(request);
                                found = true;
                                break;
                            case "POST":
                                item.Post(request);
                                found = true;
                                break;
                            case "GET":
                                item.Get(request);
                                found = true;
                                break;
                            case "DELETE":
                                item.Delete(request);
                                found = true;
                                break;
                            case "OPTIONS":
                                item.Options(request);
                                found = true;
                                break;
                        }
                    }
                    catch (ApiException e)
                    {
                        if(e.HttpBody != null) request.Respond(e.HttpBody, e.HttpStatus);
                        else request.Respond(e.HttpStatus);
                        found = true;
                    }
                }
            }
            if(!found) request.Respond(404);
        }

        /// <summary>
        /// Adds a route to the route-list.
        /// </summary>
        /// <param name="route">Route to add</param>
        public void AddRoute(IRouteHandler route)
        {
            Routes.Add(route);
        }

        /// <summary>
        /// Removes a route from the route-list.
        /// </summary>
        /// <param name="route">Route to remove</param>
        public void RemoveRoute(IRouteHandler route)
        {
            Routes.Remove(route);
        }
    }
}
