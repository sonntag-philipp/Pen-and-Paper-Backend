using MySql.Data.MySqlClient;
using PNPService.Shared.Controller.Contracts;
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
        private MySqlConnection _Connection;

        public MySQLController(IConfigController Config)
        {
            _Connection = new MySqlConnection(
                "Server=" + Config.Config.MySQL_ServerAddress + ";" +
                "Database=" + Config.Config.MySQL_Database + ";" +
                "Uid=" + Config.Config.MySQL_UserName + ";" +
                "Pwd=" + Config.Config.MySQL_Password + ";"
            );

            _Connection.Open();
        }

        public void Close()
        {
            _Connection.Close();
        }

        public string DoQuery(string SQL)
        {
            MySqlCommand cmd = new MySqlCommand(SQL, _Connection);

            MySqlDataReader reader = cmd.ExecuteReader();
            reader.Read();
            try
            {
                return reader.GetString(0);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public void DoNonQuery(string SQL)
        {
            MySqlCommand cmd = new MySqlCommand(SQL, _Connection);
        }
    }
}
