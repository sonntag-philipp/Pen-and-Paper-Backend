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
        private string _Name;
        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }


        private string _Description;
        public string Description {
            get { return _Description; }
            set { _Description = value; }
        }


        private byte _ThrowChance;
        public byte ThrowChance {
            get { return _ThrowChance; }
            set { _ThrowChance = value; }
        }

        private List<IEffect> _Effects;
        public List<IEffect> Effects {
            get { return _Effects; }
            set { _Effects = value; }
        }
    }
}
