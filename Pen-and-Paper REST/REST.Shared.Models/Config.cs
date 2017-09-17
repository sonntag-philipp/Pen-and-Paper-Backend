using REST.Shared.Models.Contracts;

namespace REST.Shared.Models
{
    public class Config : IConfig
    {
        /// <summary>
        /// The address of the MySQL Server.
        /// </summary>
        private string _MySQL_ServerAddress;
        public string MySQL_ServerAddress {
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


        private string _Http_Prefix;

        public string Http_Prefix {
            get { return _Http_Prefix; }
            set { _Http_Prefix = value; }
        }

    }
}
