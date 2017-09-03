using PNPService.Shared.Controller;
using PNPService.Shared.Controller.Contracts;
using PNPService.Shared.ViewModels.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.ViewModels
{
    public class SessionViewModel : ISessionViewModel
    {
        private IMySQLController _MySQLController;

        public IMySQLController DBController {
            get { return _MySQLController; }
            set { _MySQLController = value; }
        }

        public SessionViewModel()
        {
            DBController = new MySQLController(new ConfigController().Config);
        }

        ~SessionViewModel()
        {
            DBController.Close();
        }


        public string AddSessionID(string username, string password)
        {
            string guidString = "";
            try
            {

                if (DBController.DoQuery(
                    @"SELECT `identifier` FROM `user_data` WHERE `username`=@username AND `password`=@password",
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("username", username), new KeyValuePair<string, string>("password", password) }) != "")
                {
                    guidString = Convert.ToBase64String(Guid.NewGuid().ToByteArray());
                    guidString = guidString.Replace("=", "");
                    guidString = guidString.Replace("+", "");
                    guidString = guidString.Replace("/", "");

                    Console.WriteLine("Added: " + guidString);


                    DBController.DoQuery(
                        @"INSERT INTO `session_ids`(`session_id`, `username`) VALUES (@sid, @username)",
                        new KeyValuePair<string, string>[] {
                            new KeyValuePair<string, string>("sid", guidString),
                            new KeyValuePair<string, string>("username", username)
                        }
                    );
                }
            }
            catch (Exception)
            {
                Console.WriteLine("Exception whilst connecting to database.");
            }

            return guidString;
        }


        public bool CheckSessionID(string id, string username)
        {
            try
            {
                DBController = new MySQLController(new ConfigController().Config);

                if (DBController.DoQuery(
                    @"SELECT `identifier` FROM `session_ids` WHERE `session_id` =@sid",
                    new KeyValuePair<string, string>[] { new KeyValuePair<string, string>("sid", id) }) != "")
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
