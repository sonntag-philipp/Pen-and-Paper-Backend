using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Char
{
    public class Item : IItem
    {
        private List<IEffect> _Effects;
        public List<IEffect> Effects {
            get { return _Effects; }
            set { _Effects = value; }
        }

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

        private bool _Equippable;
        public bool Equippable {
            get { return _Equippable; }
            set { _Equippable = value; }
        }
    }
}
