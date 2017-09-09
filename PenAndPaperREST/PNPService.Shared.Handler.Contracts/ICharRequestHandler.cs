using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Handler.Contracts
{
    public interface ICharRequestHandler
    {
        void SaveCharacter(string charContent, string charName);
        string LoadCharacter(string userName, string charName);
    }
}
