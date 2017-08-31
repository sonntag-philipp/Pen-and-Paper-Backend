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


        public void HandleRequest(HttpListenerContext context)
        {
            Stream inputStream = context.Request.InputStream;

            StreamReader streamReader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding);

            Message msg = JsonConvert.DeserializeObject<Message>(streamReader.ReadToEnd());

            switch (msg.Type)
            {
                case "login":

                    //TODO: Make this better... wwwaayy better ^^
                    Console.WriteLine("+Login by " + JsonConvert.DeserializeObject<AccountData>(msg.Content).Username);

                    Guid g = Guid.NewGuid();
                    string guidString = Convert.ToBase64String(g.ToByteArray());
                    guidString = guidString.Replace("=", "");
                    guidString = guidString.Replace("+", "");

                    Console.WriteLine(guidString);

                    dynamic responseObject = new ExpandoObject();

                    responseObject.session_id = guidString;

                    string responseString = JsonConvert.SerializeObject(responseObject);

                    context.Response.StatusCode = 200;
                    context.Response.AddHeader("Access-Control-Allow-Origin", "*");

                    byte[] buffer = Encoding.UTF8.GetBytes(responseString);
                    context.Response.ContentLength64 = buffer.Length;
                    context.Response.OutputStream.Write(buffer, 0, buffer.Length);

                    context.Response.OutputStream.Close();
                    context.Response.Close();

                    break;
            }
        }



        public void StartListener()
        {
            Listener = new HttpListener();

            Listener.Prefixes.Add("http://localhost:8080/");
            Listener.Prefixes.Add("http://127.0.0.1:8080/");

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
