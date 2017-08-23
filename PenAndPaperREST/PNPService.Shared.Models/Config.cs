using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Models
{
    public class Config : IConfig
    {
        /// <summary>
        /// The address of the MySQL Server.
        /// </summary>
        private string  _MySQL_ServerAddress;
        public string  MySQL_ServerAddress {
            get { return _MySQL_ServerAddress; }
            set { _MySQL_ServerAddress = value; }
        }


        /// <summary>
        /// The username of the MySQL User.
        /// </summary>
        private string _MySQL_UserName;
        public string MySQL_UserName {
            get { return _MySQL_UserName; }
            set { _MySQL_UserName = value; }
        }


        /// <summary>
        /// The password for the MySQL User.
        /// </summary>
        private string _MySQL_Password;
        public string MySQL_Password {
            get { return _MySQL_Password; }
            set { _MySQL_Password = value; }
        }


        /// <summary>
        /// The name database on the server.
        /// </summary>
        private string _MySQL_Database;
        public string MySQL_Database {
            get { return _MySQL_Database; }
            set { _MySQL_Database = value; }
        }


        /// <summary>
        /// The port of the 
        /// </summary>
        private string _MySQL_Port;
        public string MySQL_Port {
            get { return _MySQL_Port; }
            set { _MySQL_Port = value; }
        }

        public void OpenFile()
        {
            throw new NotImplementedException();
        }

        public void WriteFile()
        {
            throw new NotImplementedException();
        }
    }
}
