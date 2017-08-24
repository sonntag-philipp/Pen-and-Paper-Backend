using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Controller.Contracts
{
    public interface ICharController
    {
        void AddChar(IChar character);
        void RemoveChar();
        void EditChar(IChar character);
    }
}
