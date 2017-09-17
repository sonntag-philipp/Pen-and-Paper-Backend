using REST.Shared.Handler.Contracts;
using REST.Shared.Controller.Contracts;
using REST.Shared.Utilities.Contracts;
using REST.Shared.Utilities;

namespace REST.Shared.Handler
{
    public class AccountRouteHandler : IRouteHandler
    {
        public IMySqlController DatabaseController {
            get { return _DatabaseController; }
            private set { _DatabaseController = value; }
        }
        private IMySqlController _DatabaseController;


        public string Route {
            get { return _Route; }
        }
        private string _Route = "/account/";


        public IApiRequest Request {
            get { return _Request; }
            private set { _Request = value; }
        }
        private IApiRequest _Request;


        public void Delete(IApiRequest request)
        {
            throw new ApiException(500);
        }

        public void Get(IApiRequest request)
        {
            throw new ApiException(500);
        }

        public void Options(IApiRequest request)
        {
            throw new ApiException(500);
        }

        public void Post(IApiRequest request)
        {
            throw new ApiException(500);
        }

        public void Put(IApiRequest request)
        {
            throw new ApiException(500);
        }
    }
}
