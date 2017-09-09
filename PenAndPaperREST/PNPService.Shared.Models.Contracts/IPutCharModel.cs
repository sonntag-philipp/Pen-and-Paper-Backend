using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Contracts
{
    public interface IPutCharModel
    {
        string SessionID { get; set; }

        string Character { get; set; }
    }
}
