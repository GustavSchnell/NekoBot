using PluginContracts;
using System.Collections.Generic;

namespace NekoBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            DiscordConnector discordConnector = new DiscordConnector(GetAvailablePlugins());
            discordConnector.Run();
        }

        private static Dictionary<string, IPlugin> GetAvailablePlugins()
        {
            var availablePlugins = new Dictionary<string, IPlugin>();
            List<IPlugin> plugins = PluginLoader<IPlugin>.LoadPlugins("Plugins");
            foreach (var item in plugins)
            {
                availablePlugins.Add(item.Name, item);
            }

            return availablePlugins;
        }
    }
}
