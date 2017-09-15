using REST.Shared.Utilities.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace REST.Shared.Utilities
{
    public class ApiRequest : IApiRequest
    {
        private string _URL;
        public string URL {
            get { return _URL; }
            set { _URL = value; }
        }

        private string _RequestedResource;
        public string RequestedResource {
            get { return _RequestedResource; }
            set { _RequestedResource = value; }
        }

        private string _RequestedTable;

        public string RequestedTable {
            get { return _RequestedTable; }
            set { _RequestedTable = value; }
        }


        private string _Method;
        public string Method {
            get { return _Method; }
            set { _Method = value; }
        }

        private HttpListenerContext _Context;
        public HttpListenerContext Context {
            get { return _Context; }
            private set { _Context = value; }
        }

        private string _Content;

        public string Content {
            get { return _Content; }
            set { _Content = value; }
        }


        public ApiRequest(HttpListenerContext ctx)
        {
            Context = ctx;
            URL = Context.Request.RawUrl;
            Method = Context.Request.HttpMethod;

            using(StreamReader reader = new StreamReader(ctx.Request.InputStream))
            {
                Content = reader.ReadToEnd();
            }

            if(URL.Split('/').Length == 3)
            {
                RequestedResource = Context.Request.RawUrl.Split('/')[2];
                RequestedTable = Context.Request.RawUrl.Split('/')[1];
            }
        }

        public void SendResponse(string response)
        {
            if (response.Trim() == "")
            {
                SendResponse(404);
                return;
            }
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Context.Response.AddHeader("Content-Type", "application/json; charset=utf-8");


            byte[] buffer = Encoding.UTF8.GetBytes(response);
            Context.Response.ContentLength64 = buffer.Length;
            Context.Response.OutputStream.Write(buffer, 0, buffer.Length);

            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }

        public void SendResponse(int status)
        {
            Context.Response.StatusCode = status;
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            
            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }

        public void SendResponse(string response, int status)
        {
            Context.Response.StatusCode = status;
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Context.Response.AddHeader("Content-Type", "application/json; charset=utf-8");

            
            byte[] buffer = Encoding.UTF8.GetBytes(response);
            Context.Response.ContentLength64 = buffer.Length;
            Context.Response.OutputStream.Write(buffer, 0, buffer.Length);

            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }

        public void SendError()
        {
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Context.Response.StatusCode = 500;
            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }
        
        public void Close()
        {
            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }
    }
}
