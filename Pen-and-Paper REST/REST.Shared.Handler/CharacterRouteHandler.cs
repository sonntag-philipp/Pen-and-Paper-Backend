using REST.Shared.Handler.Contracts;
using System;
using System.Collections.Generic;
using REST.Shared.Utilities.Contracts;
using REST.Shared.Controller.Contracts;
using REST.Shared.Utilities;
using REST.Shared.Controller;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace REST.Shared.Handler
{
    public class CharacterRouteHandler : IRouteHandler
    {
        public IMySqlController DatabaseController {
            get { return _DatabaseController; }
            private set { _DatabaseController = value; }
        }
        private IMySqlController _DatabaseController;


        public string Route {
            get { return _Route; }
        }
        private string _Route = "/character/";


        public IApiRequest Request {
            get { return _Request; }
            private set { _Request = value; }
        }
        private IApiRequest _Request;

        /// <summary>
        /// Handles the /character/ route.
        /// </summary>
        public CharacterRouteHandler()
        {
            DatabaseController = new MySqlController(new ConfigController().Config);
        }
        

        public void Delete(IApiRequest request)
        {
            throw new ApiException(500);
        }
        
        public void Get(IApiRequest request)
        {
            if (!DatabaseController.Connected) DatabaseController.Connect();

            string response = "";
            try
            {
                if(request.RequestedResources.Length == 1 || (request.RequestedResources.Length == 2 && request.RequestedResources[1] == ""))
                {
                    response = DatabaseController.DoQuery(
                        @"SELECT `content` FROM `json_characters` WHERE `unique_name`=@resourceID",
                        new KeyValuePair<string, string>[]
                        {
                    new KeyValuePair<string, string>("resourceID", request.RequestedResources[0])
                        }
                    );
                }
                else
                {
                    response = "[";
                    for (int i = 0; i < request.RequestedResources.Length; i++)
                    {
                        string dbResponse = DatabaseController.DoQuery(
                            @"SELECT `content` FROM `json_characters` WHERE `unique_name`=@resourceID",
                            new KeyValuePair<string, string>[]
                            {
                                new KeyValuePair<string, string>("resourceID", request.RequestedResources[i])
                            }
                        );

                        if(dbResponse != "")
                        {
                            response += dbResponse;
                            if (i != request.RequestedResources.Length - 1) response += ", ";
                        }
                    }
                    response += "]";
                }
            }
            catch (Exception)
            {
                throw new ApiException(503);
            }

            if (response.Trim() != "") request.Respond(response);
            else throw new ApiException(404);
        }

        public void Options(IApiRequest request)
        {
            throw new ApiException(500);
        }

        public void Post(IApiRequest request)
        {
            if (!DatabaseController.Connected) DatabaseController.Connect();

            string guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            guid = guid.Replace("=", "");
            guid = guid.Replace("+", "");
            guid = guid.Replace("/", "");
            guid = guid.Substring(0, 6);

            dynamic charJson = JObject.Parse(request.Content);
            charJson.uid = guid;
            

            try
            {
                DatabaseController.DoQuery(
                    @"INSERT INTO `json_characters` (`unique_name`, `content`) VALUES (@guid, @character)",
                    new KeyValuePair<string, string>[] {
                        new KeyValuePair<string, string>("guid", guid),
                        new KeyValuePair<string, string>("character", JsonConvert.SerializeObject(charJson))
                    }
                );
            }
            catch (Exception)
            {
                throw new ApiException(503);
            }
            charJson = null;

            dynamic response = new JObject();
            response.guid = guid;
            request.Respond(JsonConvert.SerializeObject(response));
            response = null;
        }

        public void Put(IApiRequest request)
        {
            throw new ApiException(500);
        }
    }
}
