using REST.Shared.Utilities.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace REST.Shared.Utilities
{
    public class ApiRequest : IApiRequest
    {
        public string[] RequestedResources {
            get { return _RequestedResources; }
            private set { _RequestedResources = value; }
        }
        private string[] _RequestedResources;


        public string URL {
            get { return _URL; }
            private set { _URL = value; }
        }
        private string _URL;

        public string RequestedResource {
            get { return _RequestedResource; }
            private set { _RequestedResource = value; }
        }
        private string _RequestedResource;


        public string RequestedTable {
            get { return _RequestedTable; }
            private set { _RequestedTable = value; }
        }
        private string _RequestedTable;


        public string Method {
            get { return _Method; }
            private set { _Method = value; }
        }
        private string _Method;

        public HttpListenerContext Context {
            get { return _Context; }
            private set { _Context = value; }
        }
        private HttpListenerContext _Context;


        public string Content {
            get { return _Content; }
            private set { _Content = value; }
        }
        private string _Content;


        public string Route {
            get { return _Route; }
            private set { _Route = value; }
        }
        private string _Route;



        public ApiRequest(HttpListenerContext ctx)
        {
            Context = ctx;
            URL = Context.Request.RawUrl;
            Method = Context.Request.HttpMethod;

            using(StreamReader reader = new StreamReader(ctx.Request.InputStream))
            {
                Content = reader.ReadToEnd();
            }

            if (Content.Length > 16000) throw new ApiException("Too long request.", 500);

            string[] splittedURL = URL.Split('/');

            if(splittedURL.Length >= 3)
            {
                Route = "/" + splittedURL[1] + "/";
                RequestedTable = splittedURL[1];

                RequestedResources = new string[splittedURL.Length - 2];
                Array.Copy(splittedURL, 2, RequestedResources, 0, splittedURL.Length - 2);
            }
        }
        
        public void Close()
        {
            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }

        public void Respond(string body)
        {
            Context.Response.StatusCode = 200;
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Context.Response.AddHeader("Content-Type", "application/json; charset=utf-8");


            byte[] buffer = Encoding.UTF8.GetBytes(body);
            Context.Response.ContentLength64 = buffer.Length;
            Context.Response.OutputStream.Write(buffer, 0, buffer.Length);

            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }

        public void Respond(string body, int status)
        {
            Context.Response.StatusCode = status;
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            Context.Response.AddHeader("Content-Type", "application/json; charset=utf-8");


            byte[] buffer = Encoding.UTF8.GetBytes(body);
            Context.Response.ContentLength64 = buffer.Length;
            Context.Response.OutputStream.Write(buffer, 0, buffer.Length);

            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }

        public void Respond(int status)
        {
            Context.Response.StatusCode = status;
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");

            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }

        public void Respond(int status, KeyValuePair<string, string>[] headers)
        {
            Context.Response.StatusCode = status;
            Context.Response.AddHeader("Access-Control-Allow-Origin", "*");
            
            for (int i = 0; i < headers.Length; i++)
            {
                Context.Response.AddHeader(headers[i].Key, headers[i].Value);
            }

            Context.Response.OutputStream.Close();
            Context.Response.Close();
        }
    }
}
