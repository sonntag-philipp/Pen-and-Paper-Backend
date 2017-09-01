using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Controller
{
    public class SessionController : ISessionController
    {
        private MySQLController _controller;

        public bool CheckSessionID(string id)
        {
            try
            {
                _controller = new MySQLController(new ConfigController().Config);

                if (_controller.DoQuery(
                    @"SELECT `identifier` FROM `session_ids` WHERE `session_id` =@sid", 
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("sid", id) }) != "")
                {
                    return true;
                }
                _controller.Close();

            }
            catch (Exception)
            {
                Console.WriteLine("Exception whilst connecting to database.");
            }

            return false;
        }

        public string GenerateSessionID(string username, string password)
        {
            string guidString = "";
            try
            {
                _controller = new MySQLController(new ConfigController().Config);

                if(_controller.DoQuery(
                    @"SELECT `identifier` FROM `user_data` WHERE `username`=@username AND `password`=@password",
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("username", username), new KeyValuePair<string, string>("password", password)}) != "")
                {

                    guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    guidString = guidString.Replace("=", "");
                    guidString = guidString.Replace("+", "");
                    guidString = guidString.Replace("/", "");

                    Console.WriteLine("Added: " + guidString);

                    _controller.Close();

                    _controller = new MySQLController(new ConfigController().Config);

                    _controller.DoQuery(
                        @"INSERT INTO `session_ids`(`session_id`) VALUES (@sid)", 
                        new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("sid", guidString) }
                    );

                    _controller.Close();
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception whilst connecting to database.");
            }

            return guidString;
        }

        public void RemoveSessionID(string id)
        {
            try
            {
                _controller = new MySQLController(new ConfigController().Config);

                _controller.DoQuery(
                    @"DELETE FROM `session_ids` WHERE `session_id`=@sid", 
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("sid", id) }
                );
            }
            catch (Exception)
            {
                Console.WriteLine("Exception whilst connecting to database.");
            }
        }
    }
}
