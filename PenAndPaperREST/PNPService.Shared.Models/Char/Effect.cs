using PNPService.Shared.Models.Contracts.Char;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models.Char
{
    public class Effect : IEffect
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

        private int _Strength;
        public int Strength {
            get { return _Strength; }
            set { _Strength = value; }
        }
    }
}
