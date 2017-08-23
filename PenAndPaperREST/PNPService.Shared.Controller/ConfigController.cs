using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using PNPService.Shared.Models.Contracts;
using Newtonsoft.Json;
using System.IO;

namespace PNPService.Shared.Controller
{
    public class ConfigController : IConfigController
    {
        private IConfig _Config;

        public  IConfig Config {
            get { return _Config; }
            set { _Config = value; }
        }

        public void ReadFile()
        {
            _Config = JsonConvert.DeserializeObject<IConfig>(File.ReadAllText(@".\ServerConfig.json"));
        }

        public void WriteFile()
        {
            File.WriteAllText(@".\ServerConfig.json", JsonConvert.SerializeObject(_Config));
        }
    }
}
