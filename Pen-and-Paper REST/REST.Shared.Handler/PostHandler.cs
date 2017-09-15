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
    public class PostHandler : IPostHandler
    {
        List<string> routes = new List<string>();

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

        public void HandlePost(IApiRequest request)
        {
            switch (request.RequestedTable)
            {
                case "character":
                    if (request.Content.Length >= 16000)
                    {
                        request.SendError();
                        return;
                    }

                    string guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    guid = guid.Replace("=", "");
                    guid = guid.Replace("+", "");
                    guid = guid.Replace("/", "");
                    guid = guid.Substring(0, 6);

                    MySqlController.DoQuery(
                        @"INSERT INTO `json_characters` (`unique_name`, `content`) VALUES (@guid, @character)",
                        new KeyValuePair<string, string>[] {
                            new KeyValuePair<string, string>("guid", guid),
                            new KeyValuePair<string, string>("character", request.Content)
                    });

                    request.SendResponse(guid);
                    break;
                case "account":
                    if (request.Content.Length >= 16000)
                    {
                        request.SendError();
                        return;
                    }

                    if(MySqlController.DoQuery(
                        @"SELECT `content` FROM `json_accounts` WHERE `username`=@resourceID",
                        new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("resourceID", request.RequestedResource)
                        }
                    ) == "")
                    {
                        MySqlController.DoQuery(
                        @"INSERT INTO `json_accounts` (`username`, `content`) VALUES (@resourceID, @content)",
                        new KeyValuePair<string, string>[] {
                            new KeyValuePair<string, string>("resourceID", request.RequestedResource),
                            new KeyValuePair<string, string>("content", request.Content)
                        });
                        request.SendResponse("Successful created", 200);
                    }
                    else
                    {
                        request.SendResponse("Account already exists", 500);
                    }
                    break;
            }
        }
    }
}
