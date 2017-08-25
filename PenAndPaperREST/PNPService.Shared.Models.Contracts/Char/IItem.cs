using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Contracts.Char
{
    public interface IItem
    {
        List<IEffect> ItemEffects { get; set; }
        
        string Name { get; set; }
        string Description { get; set; }

        bool Equippable { get; set; }
    }
}
