using REST.Shared.Controller;
using REST.Shared.Handler;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST.Client.Windows
{
    class Program
    {
        static void Main(string[] args)
        {
            PostHandler postHandler = new PostHandler();
            GetHandler getHandler = new GetHandler();

            HttpController httpController = new HttpController(postHandler.HandlePost, getHandler.HandleGet);
        }
    }
}
