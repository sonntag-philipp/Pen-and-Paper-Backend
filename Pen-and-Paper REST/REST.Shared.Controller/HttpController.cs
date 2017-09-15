using Newtonsoft.Json;
using REST.Shared.Controller.Contracts;
using REST.Shared.Models.Contracts;
using REST.Shared.Utilities;
using REST.Shared.Utilities.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace REST.Shared.Controller
{
    public class HttpController: IHttpController
    {

        private IConfig Config;
        private MySqlController _MySqlController;

        public delegate void requestHandle(IApiRequest Request);

        private requestHandle _postHandler;
        public requestHandle PostHandler {
            get { return _postHandler; }
            set { _postHandler = value; }
        }
        

        private requestHandle _getHandler;
        public requestHandle GetHandler {
            get { return _getHandler; }
            set { _getHandler = value; }
        }


        private HttpListener _Listener;
        public HttpListener Listener {
            get { return _Listener; }
            set { _Listener = value; }
        }

        /// <summary>
        /// Class that creates http listeners to listen for incoming api-requests.
        /// </summary>
        /// <param name="requestHandler">Void that handles a request.</param>
        public HttpController(requestHandle postHandler, requestHandle getHandler)
        {
            PostHandler = postHandler;
            GetHandler = getHandler;

            Config = new ConfigController().Config;

            _MySqlController = new MySqlController(Config);

            Listener = new HttpListener();
            Listener.Prefixes.Add(Config.Http_Prefix);

            Thread listenThread = new Thread(() => HandleLoop());
            listenThread.Start();

            Console.WriteLine("[" + DateTime.Now + "] Listening on \"" + Config.Http_Prefix + "\"");
        }

        private void HandleLoop()
        {
            Listener.Start();

            while (Listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = Listener.GetContext();
                    
                    ThreadPool.QueueUserWorkItem(o => HandleRequest(new ApiRequest(context)));
                }
                catch (Exception)
                {
                    Listener.Stop();
                }
            }
        }

        public void HandleRequest(IApiRequest request)
        {
            try
            {
                switch (request.Method)
                {
                    case "GET":
                        Console.WriteLine("[" + DateTime.Now + "][GET]  " + request.URL);
                        GetHandler(request);
                        break;

                    case "POST":
                        Console.WriteLine("[" + DateTime.Now + "][POST] " + request.URL);
                        PostHandler(request);
                        break;
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("[" + DateTime.Now + "][ERROR] Exception in Request Handler");
                Console.WriteLine(e.StackTrace);
            }
        }
    }
}
