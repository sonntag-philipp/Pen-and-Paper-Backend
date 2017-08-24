using MySql.Data.MySqlClient;
using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Controller
{
    public class MySQLController : IMySQLController
    {
        private MySqlConnection _Connection;

        public void OpenConnection(IConfigController Config)
        {
            _Connection = new MySqlConnection(
                "Server=" + Config.Config.MySQL_ServerAddress + ";" +
                "Database=" + Config.Config.MySQL_Database + ";" +
                "Uid=" + Config.Config.MySQL_UserName + ";" +
                "Pwd=" + Config.Config.MySQL_Password + ";"
                );
            
            _Connection.Open();
        }

        public void CloseConnection()
        {
            _Connection.Close();
        }

        public string DoQuery()
        {
            throw new NotImplementedException();
        }

        public void DoNonQuery()
        {
            throw new NotImplementedException();
        }
    }
}
