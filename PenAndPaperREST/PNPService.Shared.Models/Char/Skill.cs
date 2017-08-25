using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Char
{
    public class Skill : ISkill
    {
        public string Name { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public string Description { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public byte ThrowChance { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
        public List<IEffect> Effects { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }
    }
}
