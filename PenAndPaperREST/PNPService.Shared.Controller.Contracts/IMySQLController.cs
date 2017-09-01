using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PNPService.Shared.Controller.Contracts
{
    public interface IMySQLController
    {
        void Close();
        void DoNonQuery(string SQL);
        
        string DoQuery(string SQL);
    }
}
