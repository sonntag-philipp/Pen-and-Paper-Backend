using PNPService.Shared.Manager.Contracts;
using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Manager
{
    public class AccountManager : IAccountManager
    {
        public bool ExecuteLogin(string accountName, string accountPass)
        {
            throw new NotImplementedException();
        }

        public List<ICharacter> FetchAccountCharacters(string accountName)
        {
            throw new NotImplementedException();
        }
    }
}
