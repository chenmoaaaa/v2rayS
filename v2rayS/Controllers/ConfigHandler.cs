using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using v2rayS.Models;

namespace v2rayS.Controllers
{
    class ConfigHandler
    {
        private static readonly string FILE_NAME = "SConfig.json";

        private static Configuration _config;

        public static Configuration GetConfig()
        {
            if (_config == null)
            {
                _config = new Configuration();
            }
            return _config;
        }

        public static void LoadFile()
        {
            if (!File.Exists(FILE_NAME))
            {
                SaveFile();
            }
            else
            {
                string jsonString = File.ReadAllText(FILE_NAME);
                _config = JsonConvert.DeserializeObject<Configuration>(jsonString);
            }
        }

        public static void SaveFile()
        {
            string jsonString = JsonConvert.SerializeObject(_config);
            File.WriteAllText(FILE_NAME, jsonString);
        }
    }
}
