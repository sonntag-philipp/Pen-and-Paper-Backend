using REST.Shared.Controller.Contracts;
using REST.Shared.Utilities.Contracts;

namespace REST.Shared.Handler.Contracts
{
    public interface IRouteHandler
    {
        IMySqlController DatabaseController { get; }
        IApiRequest Request { get; }
        string Route { get; }

        void Put(IApiRequest request);
        void Post(IApiRequest request);
        void Get(IApiRequest request);
        void Options(IApiRequest request);
        void Delete(IApiRequest request);
    }
}
