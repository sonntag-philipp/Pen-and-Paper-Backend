using System.Collections.Generic;

namespace REST.Shared.Controller.Contracts
{
    public interface IMySqlController
    {
        string DoQuery(string SQL, KeyValuePair<string, string>[] parameters);
        void Close();
    }
}
