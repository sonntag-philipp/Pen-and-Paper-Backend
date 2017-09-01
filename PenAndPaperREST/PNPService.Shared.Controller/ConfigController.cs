using PNPService.Shared.Controller.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using System.IO;
using PNPService.Shared.Models;
using PNPService.Shared.Models.Contracts;

namespace PNPService.Shared.Controller
{
    public class ConfigController : IConfigController
    {
        /// <summary>
        /// Constructor with automatic config reading, so that you can easily use this class.
        /// </summary>
        public ConfigController()
        {
            ReadFile();
        }


        /// <summary>
        /// The object that stores all the config data.
        /// </summary>
        public IConfig Config {
            get { return _Config == null ? _Config = new Config() : _Config; }
            set { _Config = value; }
        }
        private IConfig _Config;
        


        /// <summary>
        /// Reads the content of the ServerConfig.json and puts it into the config object.
        /// </summary>
        public void ReadFile()
        {
            Config = JsonConvert.DeserializeObject<Config>(File.ReadAllText(@".\ServerConfig.json"));
        }


        /// <summary>
        /// Writes the content of the ServerConfig.json to a file called "ServerConfig.json".
        /// </summary>
        public void WriteFile()
        {
            File.WriteAllText(@".\ServerConfig.json", JsonConvert.SerializeObject(_Config));
        }
    }
}
