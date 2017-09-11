using Newtonsoft.Json;
using REST.Shared.Controller.Contracts;
using REST.Shared.Models.Contracts;
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

        public delegate void requestHandle(HttpListenerContext ctx);

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
        }

        private void HandleLoop()
        {
            Listener.Start();

            while (Listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = Listener.GetContext();

                    ThreadPool.QueueUserWorkItem(o => HandleRequest(context));
                }
                catch (Exception)
                {
                    Listener.Stop();
                }
            }
        }

        private void HandleRequest(HttpListenerContext ctx)
        {
            Console.WriteLine("+" + ctx.Request.HttpMethod + "  -- " + ctx.Request.RawUrl);

            if (ctx.Request.HttpMethod == "GET") GetHandler(ctx);

            if (ctx.Request.HttpMethod == "POST") PostHandler(ctx);
        }
    }
}
