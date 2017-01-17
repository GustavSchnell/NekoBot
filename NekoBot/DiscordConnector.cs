using PluginContracts;
using System;
using System.Collections.Generic;

namespace NekoBot
{
    public class DiscordConnector
    {
        private Dictionary<string, IPlugin> availablePlugins;

        public DiscordConnector(Dictionary<string, IPlugin> availablePlugins)
        {
            this.availablePlugins = availablePlugins;
        }

        public void Run()
        {
            throw new NotImplementedException();
        }
    }
}
