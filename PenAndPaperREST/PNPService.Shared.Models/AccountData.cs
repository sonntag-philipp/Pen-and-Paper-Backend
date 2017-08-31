using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models
{
    public class AccountData : IAccountData
    {
        private string _Username;

        public string Username {
            get { return _Username; }
            set { _Username = value; }
        }

        private string _Password;

        public string Password {
            get { return _Password; }
            set { _Password = value; }
        }
    }
}
