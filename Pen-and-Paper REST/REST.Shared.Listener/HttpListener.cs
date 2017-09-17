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
        public List<IRouteHandler> Routes {
            get { return _Routes; }
            set { _Routes = value; }
        }
        private List<IRouteHandler> _Routes;


        public Thread ListenerThread {
            get { return _ListenerThread; }
            set { _ListenerThread = value; }
        }
        private Thread _ListenerThread;


        public System.Net.HttpListener Listener {
            get { return _Listener; }
            set { _Listener = value; }
        }
        private System.Net.HttpListener _Listener;


        public HttpListener(string prefix)
        {
            Routes = new List<IRouteHandler>();
            Listener = new System.Net.HttpListener();
            Listener.Prefixes.Add(prefix);

            ListenerThread = new Thread(() => ListenThread());
        }

        public void Start()
        {
            Listener.Start();
            ListenerThread.Start();
        }

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

            foreach (IRouteHandler item in Routes)
            {
                if(item.Route == request.Route)
                {
                    try
                    {
                        switch (context.Request.HttpMethod)
                        {
                            case "PUT":
                                Console.WriteLine("[" + DateTime.Now + "][PUT]   " + request.URL);
                                item.Put(request);
                                break;
                            case "POST":
                                Console.WriteLine("[" + DateTime.Now + "][POST]  " + request.URL);
                                item.Post(request);
                                break;
                            case "GET":
                                Console.WriteLine("[" + DateTime.Now + "][GET]   " + request.URL);
                                item.Get(request);
                                break;
                            case "DELETE":
                                Console.WriteLine("[" + DateTime.Now + "][DELETE]" + request.URL);
                                item.Delete(request);
                                break;
                            case "OPTIONS":
                                Console.WriteLine("[" + DateTime.Now + "][OPTIONS]" + request.URL);
                                item.Options(request);
                                break;
                        }
                    }
                    catch (ApiException e)
                    {
                        if(e.HttpBody != null) request.Respond(e.HttpBody, e.HttpStatus);
                        else request.Respond(e.HttpStatus);
                    }
                    catch (Exception e)
                    {
                        Console.WriteLine("[" + DateTime.Now + "][ERROR] Exception in context handler.");
                        Console.WriteLine(e.StackTrace);
                    }
                }
            }
        }

        public void AddRoute(IRouteHandler route)
        {
            Routes.Add(route);
        }

        public void RemoveRoute(IRouteHandler route)
        {
            Routes.Remove(route);
        }
    }
}
