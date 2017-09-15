using REST.Shared.Controller;
using REST.Shared.Handler;
using REST.Shared.Utilities.Contracts;
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
            HttpController httpController = new HttpController(new PostHandler().HandlePost, new GetHandler().HandleGet);

        }
    }
}
