using PNPService.Shared.Models.Contracts;
using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Controller.Contracts
{
    public interface ICharController
    {
        ICharacter LoadCharacter(string characterName, IAccount account);
        void RemoveCharacter(string characterName, IAccount account);
        void SaveCharacter(ICharacter character, IAccount account);
    }
}
