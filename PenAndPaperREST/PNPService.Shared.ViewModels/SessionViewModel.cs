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
            return "";
        }


        public bool CheckSessionID(string id)
        {
            return false;
        }
    }
}
