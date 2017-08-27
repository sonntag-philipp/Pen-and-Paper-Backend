using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.IO;

namespace PNPService.Shared.Controller
{
    public class HTTPController : IHTTPController
    {
        private HttpListener _Listener;

        public HttpListener Listener {
            get { return _Listener; }
            set { _Listener = value; }
        }


        public void HandleRequest(HttpListenerContext context)
        {
            context.Response.StatusCode = 200;
            context.Response.SendChunked = true;

            Stream inputStream = context.Request.InputStream;

            StreamReader reader = new StreamReader(inputStream, context.Request.ContentEncoding);

            string requestString = reader.ReadToEnd();

            Console.WriteLine("Request: " + requestString);
        }



        public void StartListener()
        {
            Listener = new HttpListener();

            Listener.Prefixes.Add("http://localhost:4200/");
            Listener.Prefixes.Add("http://127.0.0.1:4200/");

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
                    
                }
            }
        }

        public void StopListener()
        {
            Listener.Stop();
        }
    }
}
