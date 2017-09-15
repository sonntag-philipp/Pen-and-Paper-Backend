using REST.Shared.Controller;
using REST.Shared.Handler.Contracts;
using REST.Shared.Utilities.Contracts;
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


        public void HandleGet(IApiRequest request)
        {
            //TODO: Make a list with a foreach loop and a class that automatically sends the response.

            switch (request.RequestedTable)
            {
                case "skills":
                    request.SendResponse(MySqlController.DoQuery(
                        @"SELECT `content` FROM `json_skills` WHERE `resourceID`=@resourceID",
                        new KeyValuePair<string, string>[] 
                        {
                            new KeyValuePair<string, string>("resourceID", request.RequestedResource)
                        }
                    ));
                    break;
                case "character":
                    request.SendResponse(MySqlController.DoQuery(
                        @"SELECT `content` FROM `json_characters` WHERE `unique_name`=@resourceID",
                        new KeyValuePair<string, string>[] 
                        {
                            new KeyValuePair<string, string>("resourceID", request.RequestedResource)
                        }
                    ));
                    break;
                case "account":
                    request.SendResponse(MySqlController.DoQuery(
                        @"SELECT `content` FROM `json_accounts` WHERE `username`=@resourceID",
                        new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("resourceID", request.RequestedResource)
                        }
                    ));
                    break;
            }
        }
    }
}
