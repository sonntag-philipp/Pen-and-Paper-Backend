using Newtonsoft.Json;
using PNPService.Shared.Controller;
using PNPService.Shared.Controller.Contracts;
using PNPService.Shared.Handler.Contracts;
using PNPService.Shared.Models;
using System;
using System.Collections.Generic;
using System.Dynamic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Handler
{
    public class HttpRequestHandler : IHttpRequestHandler
    {
        private List<IHTTPController> _HttpControllerList;

        public List<IHTTPController> HttpControllerList {
            get { return _HttpControllerList; }
            set { _HttpControllerList = value; }
        }


        public HttpRequestHandler()
        {
            HttpControllerList = new List<IHTTPController>();

            HTTPController controller = new HTTPController(HandleRequest);

            HttpControllerList.Add(controller);
        }

        public void HandleRequest(HttpListenerContext ctx)
        {
            StreamReader streamReader = new StreamReader(ctx.Request.InputStream, ctx.Request.ContentEncoding);

            Message msg = JsonConvert.DeserializeObject<Message>(streamReader.ReadToEnd());
            dynamic responseContent = new ExpandoObject();

            switch (msg.Type)
            {
                case "login":

                    AccountData accountData = JsonConvert.DeserializeObject<AccountData>(msg.Content);

                    LoginRequestHandler loginRequestHandler = new LoginRequestHandler();

                    

                    responseContent.session_id = loginRequestHandler.RequestVerification(accountData.Username, accountData.Password);

                    HTTPController.WriteResponse(ref ctx, responseContent);

                    break;
                case "get_char":

                    if(new LoginRequestHandler().RequestVerification(msg.SessionID))
                    {
                        CharRequestHandler charRequestHandler = new CharRequestHandler();

                        responseContent.character = charRequestHandler.LoadCharacter(msg.Content, msg.ResourceName);

                        HTTPController.WriteResponse(ref ctx, responseContent);
                    }
                    break;
                case "put_char":

                    if (new LoginRequestHandler().RequestVerification(msg.SessionID))
                    {
                        CharRequestHandler charRequestHandler = new CharRequestHandler();

                        charRequestHandler.SaveCharacter(msg.Content, msg.ResourceName);
                    }
                    break;
                default:
                    break;
            }
        }
    }
}
