using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Contracts
{
    public interface IMessage
    {
        string Type { get; set; }

        string Content { get; set; }
    }
}
