using System.Collections.Generic;

namespace REST.Shared.Controller.Contracts
{
    public interface IMySqlController
    {
        bool Connected { get; set; }

        string DoQuery(string SQL, KeyValuePair<string, string>[] parameters);
        void Close();

        void Connect();
        void Disconnect();
    }

}
