using Newtonsoft.Json;
using PluginContracts;
using System.Collections.Generic;
using System.IO;

namespace NekoBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new DiscordConnector(LoadConfig(), GetAvailablePlugins()).Connect();
        }

        private static Config LoadConfig()
        {
            const string configName = "config.json";

            if (!File.Exists(configName))
            {
                File.WriteAllText(configName, JsonConvert.SerializeObject(new Config(""), Formatting.Indented));
            }
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(configName));
        }

        private static Dictionary<string, IPlugin> GetAvailablePlugins()
        {
            var availablePlugins = new Dictionary<string, IPlugin>();
            var plugins = PluginLoader<IPlugin>.LoadPlugins("Plugins");
            foreach (var item in plugins)
            {
                availablePlugins.Add(item.Name, item);
            }

            return availablePlugins;
        }
    }
}
