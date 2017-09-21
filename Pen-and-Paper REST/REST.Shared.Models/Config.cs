using REST.Shared.Models.Contracts;

namespace REST.Shared.Models
{
    public class Config : IConfig
    {
        /// <summary>
        /// The address of the MySQL Server.
        /// </summary>
        public string MySQL_ServerAddress {
            get { return _MySQL_ServerAddress; }
            set { _MySQL_ServerAddress = value; }
        }
        private string _MySQL_ServerAddress;


        /// <summary>
        /// The username of the MySQL User.
        /// </summary>
        public string MySQL_UserName {
            get { return _MySQL_UserName; }
            set { _MySQL_UserName = value; }
        }
        private string _MySQL_UserName;


        /// <summary>
        /// The password for the MySQL User.
        /// </summary>
        public string MySQL_Password {
            get { return _MySQL_Password; }
            set { _MySQL_Password = value; }
        }
        private string _MySQL_Password;


        /// <summary>
        /// The name database on the server.
        /// </summary>
        public string MySQL_Database {
            get { return _MySQL_Database; }
            set { _MySQL_Database = value; }
        }
        private string _MySQL_Database;


        /// <summary>
        /// The port of the 
        /// </summary>
        public string MySQL_Port {
            get { return _MySQL_Port; }
            set { _MySQL_Port = value; }
        }
        private string _MySQL_Port;


        /// <summary>
        /// The Http prefix of the listener.
        /// </summary>
        public string Http_Prefix {
            get { return _Http_Prefix; }
            set { _Http_Prefix = value; }
        }
        private string _Http_Prefix;

    }
}
