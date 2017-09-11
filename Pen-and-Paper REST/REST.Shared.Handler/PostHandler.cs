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
    public class PostHandler : IPostHandler
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



        public PostHandler()
        {
            MySqlController = new MySqlController(new ConfigController().Config);
        }

        public void HandlePost(HttpListenerContext ctx)
        {
            string[] splittedURL = ctx.Request.RawUrl.Split('/');

            if (splittedURL.Length != 3)
            {
                StopRequest(ctx);
                return;
            }


            string response = "";


            StreamReader streamReader = new StreamReader(ctx.Request.InputStream, ctx.Request.ContentEncoding);

            string content = streamReader.ReadToEnd();

            switch (splittedURL[1])
            {
                case "character":


                    if (content.Length >= 16000)
                    {
                        StopRequest(ctx);
                        return;
                    }

                    string guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    guid = guid.Replace("=", "");
                    guid = guid.Replace("+", "");
                    guid = guid.Replace("/", "");

                    MySqlController.DoQuery(
                        @"INSERT INTO `json_characters` (`unique_name`, `content`) VALUES (@guid, @character)",
                        new KeyValuePair<string, string>[] {
                            new KeyValuePair<string, string>("guid", guid),
                            new KeyValuePair<string, string>("character", content)
                    });

                    response = "{\"guid\": \"" + guid + "\"}";
                    break;
            }

            if (string.IsNullOrEmpty(response.Trim()))
            {
                StopRequest(ctx);
                return;
            }

            // Writing the headers of the response
            ctx.Response.StatusCode = 200;
            ctx.Response.AddHeader("Access-Control-Allow-Origin", "*");
            ctx.Response.AddHeader("Content-Type", "text/html; charset=utf-8");


            // Writing the body of the response
            byte[] buffer = Encoding.UTF8.GetBytes(response);
            ctx.Response.ContentLength64 = buffer.Length;
            ctx.Response.OutputStream.Write(buffer, 0, buffer.Length);

            ctx.Response.OutputStream.Close();
            ctx.Response.Close();
        }

        private void StopRequest(HttpListenerContext ctx)
        {
            ctx.Response.StatusCode = 500;
            ctx.Response.AddHeader("Access-Control-Allow-Origin", "*");
            ctx.Response.AddHeader("Content-Type", "text/html; charset=utf-8");

            // Writing the body of the response
            byte[] buffer = Encoding.UTF8.GetBytes("<h1>500 - Database Error</h1>");
            ctx.Response.ContentLength64 = buffer.Length;
            ctx.Response.OutputStream.Write(buffer, 0, buffer.Length);

            ctx.Response.OutputStream.Close();
            ctx.Response.Close();
        }
    }
}
