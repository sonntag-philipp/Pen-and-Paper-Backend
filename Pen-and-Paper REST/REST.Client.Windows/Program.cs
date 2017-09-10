using REST.Shared.Controller;
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
            HttpController http = new HttpController(args == null ? args[0] : "http://localhost:8080/");
        }
    }
}
