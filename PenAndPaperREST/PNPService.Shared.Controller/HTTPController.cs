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
            StreamReader streamReader = new StreamReader(context.Request.InputStream, context.Request.ContentEncoding);

            Message msg = JsonConvert.DeserializeObject<Message>(streamReader.ReadToEnd());

            switch (msg.Type)
            {
                case "login":
                    AccountData userData = JsonConvert.DeserializeObject<AccountData>(msg.Content);
                    
                    dynamic responseContent = new ExpandoObject();
                    responseContent.session_id = new SessionController().GenerateSessionID(userData.Username, userData.Password);
                    
                    WriteResponse(ref context, responseContent);
                    break;
            }
        }

        private void WriteResponse(ref HttpListenerContext ctx, ExpandoObject responseContent)
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



        public void StartListener()
        {
            Listener = new HttpListener();

            Listener.Prefixes.Add("http://localhost:8080/");

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
