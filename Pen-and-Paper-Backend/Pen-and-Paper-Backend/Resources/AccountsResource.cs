using Grapevine.Interfaces.Server;
using Grapevine.Server.Attributes;
using Grapevine.Shared;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PnP_Backend.Resources
{
    [RestResource(BasePath = "/accounts")]
    public class AccountsResource
    {
        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/get")]
        public IHttpContext GetAccount(IHttpContext ctx)
        {
            return ctx;
        }

        [RestRoute(HttpMethod = HttpMethod.GET, PathInfo = "/get")]
        public IHttpContext GetAccountTwo(IHttpContext ctx)
        {
            ctx.Response.SendResponse(Encoding.UTF8.GetBytes("Antwort auf accounts/get"));
            return ctx;
        }
    }
}
