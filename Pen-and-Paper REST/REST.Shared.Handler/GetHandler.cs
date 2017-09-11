using REST.Shared.Controller;
using REST.Shared.Handler.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace REST.Shared.Handler
{
    public class GetHandler : IGetHandler
    {
        private HttpController _Controller;
        public HttpController HttpController {
            get { return _Controller; }
            set { _Controller = value; }
        }

        private MySqlController _MySqlController;

        public MySqlController MySqlController {
            get { return _MySqlController; }
            set { _MySqlController = value; }
        }

        public GetHandler()
        {
            MySqlController = new MySqlController(new ConfigController().Config);
        }


        public void HandleGet(HttpListenerContext ctx)
        {
            string[] splittedURL = ctx.Request.RawUrl.Split('/');

            if (splittedURL.Length != 3)
            {
                StopRequest(ctx);
                return;
            }


            string response = "";

            switch (splittedURL[1])
            {
                case "skills":
                    response = MySqlController.DoQuery(
                        @"SELECT `content` FROM `json_skills` WHERE `resourceID`=@resourceID",
                        new KeyValuePair<string, string>[] {
                                    new KeyValuePair<string, string>("resourceID", splittedURL[2])
                    });
                    break;
                case "character":
                    response = MySqlController.DoQuery(
                        @"SELECT `content` FROM `json_characters` WHERE `unique_name`=@guid",
                        new KeyValuePair<string, string>[] {
                                    new KeyValuePair<string, string>("guid", splittedURL[2])
                    });
                    break;
            }

            if (string.IsNullOrEmpty(response.Trim()))
            {
                StopRequest(ctx);
                return;
            }

            // Writing the headers of the response
            
            ctx.Response.AddHeader("Access-Control-Allow-Origin", "*");
            ctx.Response.AddHeader("Content-Type", "application/json; charset=utf-8");


            // Writing the body of the response
            byte[] buffer = Encoding.UTF8.GetBytes(response);
            ctx.Response.ContentLength64 = buffer.Length;
            ctx.Response.OutputStream.Write(buffer, 0, buffer.Length);

            ctx.Response.OutputStream.Close();
            ctx.Response.Close();
        }

        private void StopRequest(HttpListenerContext ctx)
        {
            ctx.Response.StatusCode = 404;
            ctx.Response.AddHeader("Access-Control-Allow-Origin", "*");
            ctx.Response.AddHeader("Content-Type", "text/html; charset=utf-8");

            // Writing the body of the response
            byte[] buffer = Encoding.UTF8.GetBytes("<h1>404 - Not found</h1>");
            ctx.Response.ContentLength64 = buffer.Length;
            ctx.Response.OutputStream.Write(buffer, 0, buffer.Length);

            ctx.Response.OutputStream.Close();
            ctx.Response.Close();
        }
    }
}
