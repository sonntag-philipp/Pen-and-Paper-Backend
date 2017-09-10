using Newtonsoft.Json;
using REST.Shared.Controller.Contracts;
using REST.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace REST.Shared.Controller
{
    public class HttpController: IHttpController
    {
        private HttpListener _Listener;

        private IConfig Config;
        private MySqlController _MySqlController;

        public HttpListener Listener {
            get { return _Listener; }
            set { _Listener = value; }
        }

        /// <summary>
        /// Class that creates http listeners to listen for incoming api-requests.
        /// </summary>
        /// <param name="requestHandler">Void that handles a request.</param>
        public HttpController(string prefixDomain)
        {
            Config = new ConfigController().Config;

            _MySqlController = new MySqlController(Config);

            Listener = new HttpListener();

            Listener.Prefixes.Add(prefixDomain);

            Thread listenerThread = new Thread(() => HandleLoop());

            listenerThread.Start();
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
            Console.WriteLine("Raw URL: " + ctx.Request.RawUrl);

            string rawURL = ctx.Request.RawUrl;

            string[] splittedUrl = rawURL.Split('/');
            Console.WriteLine("Splitted Length: " + splittedUrl.Length);
            Console.WriteLine("Splitted URL: " + splittedUrl[1]);


            string resourceID = "default";
            string databaseID = "";
            string response = "not-found";

            if(splittedUrl.Length >= 3)
            {
                resourceID = splittedUrl[2];
                databaseID = splittedUrl[1];

                switch (databaseID)
                {
                    case "skills":
                        response = _MySqlController.DoQuery(
                            @"SELECT `content` FROM `json_skills` WHERE `resourceID`=@resourceID",
                            new KeyValuePair<string, string>[] {
                                new KeyValuePair<string, string>("resourceID", resourceID)
                        });
                        break;

                    default:
                        break;
                }


            }

            WriteResponse(ctx, response);
        }

        static public void WriteResponse(HttpListenerContext ctx, string response)
        {
            if (response == "not-found" || response == "") ctx.Response.StatusCode = 404;
            else ctx.Response.StatusCode = 200;

            

            ctx.Response.AddHeader("Access-Control-Allow-Origin", "*");

            byte[] buffer = Encoding.UTF8.GetBytes(response);
            ctx.Response.ContentLength64 = buffer.Length;
            ctx.Response.OutputStream.Write(buffer, 0, buffer.Length);

            ctx.Response.OutputStream.Close();
            ctx.Response.Close();
        }
    }
}
