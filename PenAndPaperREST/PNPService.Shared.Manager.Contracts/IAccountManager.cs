using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Manager.Contracts
{
    public interface IAccountManager
    {
        bool ExecuteLogin(string accountName, string accountPass);

        List<ICharacter> FetchAccountCharacters(string accountName);
    }
}
