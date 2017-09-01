using PNPService.Shared.Controller.Contracts;
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
        IMySQLController DBController { get; set; }

        string Login(string accountName, string accountPass);
        void Logout(string session_id);

        List<ICharacter> FetchAccountCharacters(string accountName);
    }
}
