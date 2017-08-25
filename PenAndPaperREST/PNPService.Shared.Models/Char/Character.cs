using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Char
{
    public class Character : ICharacter
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Nation { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Age { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Height { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Profession { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Religion { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Level { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public int Experience { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IItem> Inventory { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IItem> Equipped { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IEffect> Effects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<ISkill> Skills { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
