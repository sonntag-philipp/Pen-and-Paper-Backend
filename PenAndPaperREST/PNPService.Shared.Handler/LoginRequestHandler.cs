using PNPService.Shared.Controller;
using PNPService.Shared.Handler.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Handler
{
    public class LoginRequestHandler : ILoginRequestHandler
    {
        private MySQLController _MySQLController; 


        public LoginRequestHandler()
        {
            _MySQLController = new MySQLController(new ConfigController().Config);
        }

        public void RequestLogout(string SessionID)
        {
            
        }

        public string RequestVerification(string username, string password)
        {
            string sessionID = "";
            try
            {

                if (_MySQLController.DoQuery(
                    @"SELECT `identifier` FROM `user_data` WHERE `username`=@username AND `password`=@password",
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("username", username), new KeyValuePair<string, string>("password", password) }) != "")
                {
                    sessionID = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    sessionID = sessionID.Replace("=", "");
                    sessionID = sessionID.Replace("+", "");
                    sessionID = sessionID.Replace("/", "");

                    Console.WriteLine("Added: " + sessionID);


                    _MySQLController.DoQuery(
                        @"INSERT INTO `session_ids`(`session_id`, `username`) VALUES (@sid, @username)",
                        new KeyValuePair<string, string>[] {
                            new KeyValuePair<string, string>("sid", sessionID),
                            new KeyValuePair<string, string>("username", username)
                        }
                    );
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception whilst connecting to database.");
            }

            return sessionID;
        }

        public bool RequestVerification(string sessionID)
        {
            try
            {
                _MySQLController = new MySQLController(new ConfigController().Config);

                if (_MySQLController.DoQuery(
                    @"SELECT `identifier` FROM `session_ids` WHERE `session_id` =@sid",
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("sid", sessionID) }) != "")
                {
                    return true;
                }

            }
            catch (Exception)
            {
                Console.WriteLine("Exception whilst connecting to database.");
            }

            return false;
        }
    }
}
