using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Contracts
{
    public interface IGetCharModel
    {
        string Username { get; set; }
        string CharacterName { get; set; }

        string SessionID { get; set; }
    }
}
