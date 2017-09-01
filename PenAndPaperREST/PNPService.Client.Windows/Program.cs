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
            MySQLController sc = new MySQLController(new ConfigController());
            
            string query = @"SELECT `session_id` FROM `session_ids` WHERE `identifier` = '1'";


            Console.WriteLine(sc.DoQuery(query));
            sc.Close();


            HTTPController httpController = new HTTPController();

            httpController.StartListener();
        }
    }
}
