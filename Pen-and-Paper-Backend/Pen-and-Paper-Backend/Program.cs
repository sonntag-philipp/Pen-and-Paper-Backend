using Grapevine.Server;
using System;
using PnP_Backend.Resources;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Pen_and_Paper_Backend
{
    class Program
    {
        static void Main(string[] args)
        {
            using (RestServer server = new RestServer())
            {
                server.LogToConsole().Start();
                Console.ReadLine();
                server.Stop();
            }
        }
    }
}
