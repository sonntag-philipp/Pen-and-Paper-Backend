using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models
{
    public class Message : IMessage
    {
        private string _Type;

        public string Type {
            get { return _Type; }
            set { _Type = value; }
        }


        private string _SessionID;

        public string SessionID {
            get { return _SessionID; }
            set { _SessionID = value; }
        }


        private string _ResourceName;

        public string ResourceName {
            get { return _ResourceName; }
            set { _ResourceName = value; }
        }


        private string _Content;

        public string Content {
            get { return _Content; }
            set { _Content = value; }
        }
    }
}
