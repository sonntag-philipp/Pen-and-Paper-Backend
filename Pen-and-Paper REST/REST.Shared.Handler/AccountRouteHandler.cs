using REST.Shared.Handler.Contracts;
using REST.Shared.Controller.Contracts;
using REST.Shared.Utilities.Contracts;
using REST.Shared.Utilities;
using REST.Shared.Controller;
using System.Collections.Generic;
using System;

namespace REST.Shared.Handler
{
    public class AccountRouteHandler : IRouteHandler
    {
        public IMySqlController DatabaseController {
            get { return _DatabaseController; }
            private set { _DatabaseController = value; }
        }
        private IMySqlController _DatabaseController;


        public string Route {
            get { return _Route; }
        }
        private string _Route = "/account/";


        public IApiRequest Request {
            get { return _Request; }
            private set { _Request = value; }
        }
        private IApiRequest _Request;

        /// <summary>
        /// Handles the /account/ route.
        /// </summary>
        public AccountRouteHandler()
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
                response = DatabaseController.DoQuery(
                    @"SELECT `content` FROM `json_accounts` WHERE `username` = @username",
                    new KeyValuePair<string, string>[]
                    {
                    new KeyValuePair<string, string>("username", request.RequestedResources[0])
                    }
                );
            }
            catch (System.Exception)
            {
                throw new ApiException(503);
            }
            
            if (response.Trim() != "") request.Respond(response);
            else throw new ApiException(404);
        }

        public void Options(IApiRequest request)
        {
            request.Respond(200, new KeyValuePair<string, string>[]
            {
                new KeyValuePair<string, string>("Access-Control-Allow-Methods", "PUT, POST, GET"),
                new KeyValuePair<string, string>("Access-Control-Allow-Headers", "Content-Type")
            });
        }

        public void Post(IApiRequest request)
        {
            if (!DatabaseController.Connected) DatabaseController.Connect();
            try
            {
                if(DatabaseController.DoQuery(
                    @"SELECT `content` FROM `json_accounts` WHERE `username` = @username",
                    new KeyValuePair<string, string>[]
                    {
                    new KeyValuePair<string, string>("username", request.RequestedResources[0])
                    }
                ) == "")
                {
                    DatabaseController.DoQuery(
                    @"INSERT INTO `json_accounts`(`username`, `content`) VALUES (@username,@content)",
                    new KeyValuePair<string, string>[]
                        {
                            new KeyValuePair<string, string>("username", request.RequestedResources[0]),
                            new KeyValuePair<string, string>("content", request.Content)
                        }
                    );
                    request.Respond(200);
                }
                else
                {
                    request.Respond(403);
                }
            }
            catch (Exception)
            {
                throw new ApiException(503);
            }
        }

        public void Put(IApiRequest request)
        {
            if (!DatabaseController.Connected) DatabaseController.Connect();
            DatabaseController.DoQuery(
                @"UPDATE `json_accounts` SET `content` = @content WHERE `json_accounts`.`username` = @username",
                new KeyValuePair<string, string>[]
                {
                    new KeyValuePair<string, string>("username", request.RequestedResources[0]),
                    new KeyValuePair<string, string>("content", request.Content)
                }
            );
            request.Respond(200);
        }
    }
}
