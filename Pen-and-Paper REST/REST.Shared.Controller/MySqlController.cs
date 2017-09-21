using MySql.Data.MySqlClient;
using REST.Shared.Controller.Contracts;
using REST.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.Data;

namespace REST.Shared.Controller
{
    public class MySqlController : IMySqlController
    {
        private MySqlConnection _connection;
        private MySqlDataReader _reader;

        /// <summary>
        /// Creates a connection to the in the config specified mysql server.
        /// </summary>
        /// <param name="Config">Server config</param>
        public MySqlController(IConfig Config)
        {
            _connection = new MySqlConnection(
                "Server=" + Config.MySQL_ServerAddress + ";" +
                "Database=" + Config.MySQL_Database + ";" +
                "Uid=" + Config.MySQL_UserName + ";" +
                "Pwd=" + Config.MySQL_Password + ";"
            );

            _connection.Open();
        }

        ~MySqlController()
        {
            Close();
        }

        /// <summary>
        /// Closes the connection to the database.
        /// </summary>
        public void Close()
        {
            _reader.Close();
            _connection.Close();
        }

        /// <summary>
        /// Reconnects to the database.
        /// </summary>
        public void Connect()
        {
            _connection.Open();
        }

        public bool Connected {
            get {
                if (_connection.State == ConnectionState.Broken) return false;
                if (_connection.State == ConnectionState.Closed) return false;
                return true;
            }
            set { _Connected = value; }
        }
        private bool _Connected;


        public void Disconnect()
        {
            _connection.Close();
        }

        /// <summary>
        /// Executes a sql-query.
        /// </summary>
        /// <param name="SQL">The SQL Query</param>
        /// <param name="parameters">The Parameters used in the query</param>
        /// <returns>The return value of the query</returns>
        public string DoQuery(string SQL, KeyValuePair<string, string>[] parameters)
        {
            try
            {
                MySqlCommand cmd = new MySqlCommand(SQL, _connection);

                for (int i = 0; i < parameters.Length; i++)
                {
                    cmd.Parameters.Add(new MySqlParameter(parameters[i].Key, parameters[i].Value));
                }

                _reader = cmd.ExecuteReader();
                _reader.Read();

                string returnVal = _reader.GetString(0);

                _reader.Close();

                return returnVal;
            }
            catch (Exception)
            {
                _reader.Close();
                return "";
            }
        }
    }
}
