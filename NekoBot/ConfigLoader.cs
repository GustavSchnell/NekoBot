using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace NekoBot
{
    public class ConfigLoader
    {
        public static Config Get()
        {
            try
            {
                if (!File.Exists("config.json"))
                {
                    string json = JsonConvert.SerializeObject(GetDefaultConfig(), Formatting.Indented);
                    File.WriteAllText("config.json", json);
                }

                return JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static Config GetDefaultConfig()
        {
            return new Config("email", "password", new List<string> { "admin", "mod" }, new List<string> { "nekobot", "general" });
        }
    }
}
