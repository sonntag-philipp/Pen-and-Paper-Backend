using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNPService.Shared.Models.Contracts.Char;

namespace PNPService.Shared.Models
{
    public class GetCharModel : IGetCharModel
    {
        private string _SessionID;

        public string SessionID {
            get { return _SessionID; }
            set { _SessionID = value; }
        }


        private ICharacter _character;

        public ICharacter Character {
            get { return _character; }
            set { _character = value; }
        }

        private string _Username;

        public string Username {
            get { return _Username; }
            set { _Username = value; }
        }

        private string _CharacterName;

        public string CharacterName {
            get { return _CharacterName; }
            set { _CharacterName = value; }
        }

    }
}
