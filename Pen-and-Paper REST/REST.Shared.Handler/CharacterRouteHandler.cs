using REST.Shared.Handler.Contracts;
using System;
using System.Collections.Generic;
using REST.Shared.Utilities.Contracts;
using REST.Shared.Controller.Contracts;
using REST.Shared.Utilities;
using REST.Shared.Controller;

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
            string response = "";
            try
            {
                response = DatabaseController.DoQuery(
                    @"SELECT `content` FROM `json_characters` WHERE `unique_name`=@resourceID",
                    new KeyValuePair<string, string>[]
                    {
                    new KeyValuePair<string, string>("resourceID", request.RequestedResource)
                    }
                );
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
            string guid = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
            guid = guid.Replace("=", "");
            guid = guid.Replace("+", "");
            guid = guid.Replace("/", "");
            guid = guid.Substring(0, 6);

            try
            {
                DatabaseController.DoQuery(
                    @"INSERT INTO `json_characters` (`unique_name`, `content`) VALUES (@guid, @character)",
                    new KeyValuePair<string, string>[] {
                    new KeyValuePair<string, string>("guid", guid),
                    new KeyValuePair<string, string>("character", request.Content)
                    }
                );
            }
            catch (Exception)
            {
                throw new ApiException(503);
            }
            request.Respond("{\"guid\": \"" + guid + "\"}");
            
        }

        public void Put(IApiRequest request)
        {
            throw new ApiException(500);
        }
    }
}
