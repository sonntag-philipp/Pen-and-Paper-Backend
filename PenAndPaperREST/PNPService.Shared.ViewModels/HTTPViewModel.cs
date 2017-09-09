using PNPService.Shared.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;
using PNPService.Shared.Controller.Contracts;
using PNPService.Shared.Controller;
using System.IO;
using PNPService.Shared.Models;
using Newtonsoft.Json;
using System.Dynamic;
using PNPService.Shared.Models.Char;

namespace PNPService.Shared.ViewModels
{
    public class HTTPViewModel : IHTTPViewModel
    {
        private List<IHTTPController> _HttpControllerList;

        public List<IHTTPController> HttpControllerList {
            get { return _HttpControllerList; }
            set { _HttpControllerList = value; }
        }

        /// <summary>
        /// View Model that handles the events emitted by the HTTPController.
        /// </summary>
        public HTTPViewModel()
        {
            HttpControllerList = new List<IHTTPController>();

            HTTPController controller = new HTTPController(HandleRequest);

            HttpControllerList.Add(controller);
        }


        /// <summary>
        /// Void that handles the incoming http contexts.
        /// </summary>
        /// <param name="ctx">Context of the listener.</param>
        public void HandleRequest(HttpListenerContext ctx)
        {
            StreamReader streamReader = new StreamReader(ctx.Request.InputStream, ctx.Request.ContentEncoding);

            Message msg = JsonConvert.DeserializeObject<Message>(streamReader.ReadToEnd());

            switch (msg.Type)
            {
                case "login":

                    // LoginRequestHandler loginRequestHandler = new LoginRequestHandler();

                    break;
                case "get-char":
                    HandleCharGet(ref ctx, ref msg);
                    break;
                case "put-char":
                    HandleCharPut(ref ctx, ref msg);
                    break;
            }

        }

        private void HandleCharPut(ref HttpListenerContext ctx, ref Message msg)
        {
            PutCharModel pcModel = JsonConvert.DeserializeObject<PutCharModel>(msg.Content);

            if (new SessionViewModel().CheckSessionID(pcModel.SessionID))
            {
               //  Character charModel = (Character) pcModel.Character;


                try
                {
                    // File.WriteAllText()JsonConvert.DeserializeObject<Character>(File.ReadAllText(@".\characters\" + gcModel.Username + @"\" + gcModel.CharacterName + ".json"));
                }
                catch (Exception)
                {
                   // charModel = new Character();
                   // charModel.Name = "Character was not found";
                }

                //dynamic responseContent = new ExpandoObject();
                //responseContent.Character = charModel;

                //HTTPController.WriteResponse(ref ctx, responseContent);

            }
        }

        private void HandleCharGet(ref HttpListenerContext ctx, ref Message msg)
        {
            GetCharModel gcModel = JsonConvert.DeserializeObject<GetCharModel>(msg.Content);

            if(new SessionViewModel().CheckSessionID(gcModel.SessionID))
            {
                Character charModel;
                try
                {
                    charModel = JsonConvert.DeserializeObject<Character>(File.ReadAllText(@".\characters\" + gcModel.Username + @"\" + gcModel.CharacterName + ".json"));
                }
                catch (Exception)
                {
                    charModel = new Character();
                    charModel.Name = "Character was not found";
                }

                dynamic responseContent = new ExpandoObject();
                responseContent.Character = charModel;

                HTTPController.WriteResponse(ref ctx, responseContent);
                
            }
        }

        private void HandleLoginRequest(ref HttpListenerContext ctx, ref Message msg)
        {
            AccountData userData = JsonConvert.DeserializeObject<AccountData>(msg.Content);
            
            




            dynamic responseContent = new ExpandoObject();
            responseContent.session_id = new SessionViewModel().AddSessionID(userData.Username, userData.Password);

            HTTPController.WriteResponse(ref ctx, responseContent);
        }
    }
}
