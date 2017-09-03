using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using System.Threading;
using System.IO;
using Newtonsoft.Json;
using PNPService.Shared.Models;
using System.Dynamic;

namespace PNPService.Shared.Controller
{
    public class HTTPController : IHTTPController
    {
        private HttpListener _Listener;

        public HttpListener Listener {
            get { return _Listener; }
            set { _Listener = value; }
        }

        // The delegate type that handles a request
        public delegate void RequestHandle(HttpListenerContext ctx);

        /// <summary>
        /// Class that creates http listeners to listen for incoming api-requests.
        /// </summary>
        /// <param name="requestHandler">Void that handles a request.</param>
        public HTTPController(RequestHandle requestHandler)
        {
            Listener = new HttpListener();

            Listener.Prefixes.Add("http://localhost:8080/");

            Thread listenerThread = new Thread(() => HandleLoop(requestHandler));

            listenerThread.Start();
        }

        private void HandleLoop(RequestHandle requestHandle)
        {
            Listener.Start();

            while (Listener.IsListening)
            {
                try
                {
                    HttpListenerContext context = Listener.GetContext();

                    ThreadPool.QueueUserWorkItem(o => requestHandle(context));
                }
                catch (Exception)
                {
                    Listener.Stop();
                }
            }
        }

        static public void WriteResponse(ref HttpListenerContext ctx, ExpandoObject responseContent)
        {
            string responseString = JsonConvert.SerializeObject(responseContent);

            ctx.Response.StatusCode = 200;
            ctx.Response.AddHeader("Access-Control-Allow-Origin", "*");

            byte[] buffer = Encoding.UTF8.GetBytes(responseString);
            ctx.Response.ContentLength64 = buffer.Length;
            ctx.Response.OutputStream.Write(buffer, 0, buffer.Length);

            ctx.Response.OutputStream.Close();
            ctx.Response.Close();
        }
    }
}
