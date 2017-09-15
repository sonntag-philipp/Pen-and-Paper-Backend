using REST.Shared.Utilities.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace REST.Shared.Controller.Contracts
{
    public interface IHttpController
    {
        void HandleRequest(IApiRequest request);

    }
}
