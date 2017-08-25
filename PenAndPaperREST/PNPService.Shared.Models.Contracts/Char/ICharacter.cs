using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Contracts.Char
{
    public interface ICharacter
    {
        /// <summary>
        /// General Data about the character
        /// </summary>
        string Name { get; set; }
        string Nation { get; set; }
        string Age { get; set; }
        string Height { get; set; }
        string Profession { get; set; }
        string Religion { get; set; }

        /// <summary>
        /// The default status effects of the character
        /// </summary>
        int Level { get; set; }
        int Experience { get; set; }

        /// <summary>
        /// The inventory of the character where the items are stored.
        /// </summary>
        List<IItem> Inventory { get; set; }

        /// <summary>
        /// A list of the items that are equipped.
        /// </summary>
        List<IItem> Equipped { get; set; }

        List<IEffect> Effects { get; set; }

        List<ISkill> Skills { get; set; }
    }
}
