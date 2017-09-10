using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST.Shared.Models.Contracts
{
    public interface IConfig
    {
        // MySQL Data.
        string MySQL_ServerAddress { get; set; }
        string MySQL_UserName { get; set; }
        string MySQL_Password { get; set; }
        string MySQL_Database { get; set; }
        string MySQL_Port { get; set; }
    }
}
