using REST.Shared.Controller;
using REST.Shared.Handler;
using REST.Shared.Listener;

namespace REST.Client.Windows
{
    class Program
    {
        static void Main(string[] args)
        {
            HttpListener Listener = new HttpListener(new ConfigController().Config.Http_Prefix);

            Listener.AddRoute(new CharacterRouteHandler());
            Listener.AddRoute(new AccountRouteHandler());
            Listener.Start();

        }
    }
}
