using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Contracts.Char
{
    public interface IEffect
    {
        string Name { get; set; }
        string Description { get; set; }
        
        int Strength { get; set; }
    }
}
