using MySql.Data.MySqlClient;
using PNPService.Shared.Controller.Contracts;
using PNPService.Shared.Models.Contracts;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Controller
{
    public class MySQLController : IMySQLController
    {
        private MySqlConnection _connection;
        private MySqlDataReader _reader;

        public MySQLController(IConfig Config)
        {
            _connection = new MySqlConnection(
                "Server=" + Config.MySQL_ServerAddress + ";" +
                "Database=" + Config.MySQL_Database + ";" +
                "Uid=" + Config.MySQL_UserName + ";" +
                "Pwd=" + Config.MySQL_Password + ";"
            );

            _connection.Open();
        }

        ~MySQLController()
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

                return _reader.GetString(0);
            }
            catch (Exception)
            {
                return "";
            }
        }
    }
}
