using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models
{
    public class Response : IResponse
    {
        private string _JsonData;

        public string JsonData {
            get { return _JsonData; }
            set { _JsonData = value; }
        }
    }
}
