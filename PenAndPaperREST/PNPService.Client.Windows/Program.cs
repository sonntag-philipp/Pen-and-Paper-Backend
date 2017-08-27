using PNPService.Shared.Controller;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Client.Windows
{
    class Program
    {
        static void Main(string[] args)
        {
            /* ConfigController c = new ConfigController();

            c.Config.MySQL_ServerAddress = "localhost";
            c.Config.MySQL_UserName = "pap_backend";
            c.Config.MySQL_Password = "uwTXRR9Tgm7OSLEN";
            c.Config.MySQL_Database = "pap_service";

            MySQLController sc = new MySQLController();

            sc.OpenConnection(c);

            Console.ReadKey(); */

            HTTPController httpController = new HTTPController();

            httpController.StartListener();

        }
    }
}
