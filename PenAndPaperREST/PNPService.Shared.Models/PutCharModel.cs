using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNPService.Shared.Models.Contracts.Char;

namespace PNPService.Shared.Models
{
    public class PutCharModel : IPutCharModel
    {
        private string _SessionID;

        public string SessionID {
            get { return _SessionID; }
            set { _SessionID = value; }
        }


        private string _Character;

        public string Character {
            get { return _Character; }
            set { _Character = value; }
        }
    }
}
