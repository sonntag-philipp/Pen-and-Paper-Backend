using REST.Shared.Handler;
using REST.Shared.Listener;

namespace REST.Client.Windows
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener Listener = new HttpListener("http://localhost:8080/");

            Listener.AddRoute(new CharacterRouteHandler());
            Listener.AddRoute(new AccountRouteHandler());

            Listener.Start();
        }
    }
}
