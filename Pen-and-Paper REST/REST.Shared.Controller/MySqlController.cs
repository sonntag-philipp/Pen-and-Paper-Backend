using MySql.Data.MySqlClient;
using REST.Shared.Controller.Contracts;
using REST.Shared.Models.Contracts;
using System;
using System.Collections.Generic;

namespace REST.Shared.Controller
{
    public class MySqlController : IMySqlController
    {
        private MySqlConnection _connection;
        private MySqlDataReader _reader;

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

        public void Close()
        {
            _reader.Close();
            _connection.Close();
        }

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
