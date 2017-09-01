using PNPService.Shared.Controller;
using PNPService.Shared.Manager.Contracts;
using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNPService.Shared.Controller.Contracts;

namespace PNPService.Shared.Manager
{
    public class AccountManager : IAccountManager
    {
        private IMySQLController _DBController;

        public IMySQLController DBController {
            get { return _DBController; }
            set { _DBController = value; }
        }

        public string Login(string accountName, string accountPass)
        {
            return "";
        }

        public void Logout(string session_id)
        {

        }

        public List<ICharacter> FetchAccountCharacters(string accountName)
        {
            throw new NotImplementedException();
        }
    }
}
