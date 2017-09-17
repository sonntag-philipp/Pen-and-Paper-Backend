using REST.Shared.Utilities.Contracts;
using System.IO;
using System.Net;
using System.Text;

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

        private string _Route;

        public string Route {
            get { return _Route; }
            set { _Route = value; }
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

            if (Content.Length > 16000) throw new ApiException("Too long request.", 500);

            if(URL.Split('/').Length == 3)
            {
                Route = "/" + URL.Split('/')[1] + "/";
                RequestedResource = URL.Split('/')[2];
                RequestedTable = URL.Split('/')[1];
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
    }
}
