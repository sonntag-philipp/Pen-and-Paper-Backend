using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models
{
    public class Account : IAccount
    {
        private string _Name;

        public string Name {
            get { return _Name; }
            set { _Name = value; }
        }

    }
}
