using Discord;
using PluginContracts;
using System;
using System.Collections.Generic;

namespace NekoBot
{
    public class DiscordConnector
    {
        private Dictionary<string, IPlugin> availablePlugins;
        private DiscordClient client;
        private Config config;

        public DiscordConnector(Config config, Dictionary<string, IPlugin> availablePlugins)
        {
            this.availablePlugins = availablePlugins;
            this.config = config;
            client = new DiscordClient();
            client.Log.Message += Log_Message;
            client.MessageReceived += Client_MessageReceived;
        }

        public void Connect()
        {
            client.Log.Info("NekoBot", "Loading Plugins...");

            foreach (var plugin in availablePlugins)
            {
                plugin.Value.Connect(client);
                client.Log.Info("NekoBot", $"{plugin.Key} loaded");
            }
            client.Log.Info("NekoBot", "All Plugins loaded");

            client.ExecuteAndWait(async () =>
            {
                await client.Connect(config.BotAccountToken, TokenType.Bot);
            });
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            if (!e.Message.IsAuthor && e.Message.Text.StartsWith("/help"))
            {
                e.Channel.SendMessage("https://raw.githubusercontent.com/dreanor/NekoBot/master/NekoBot/commands.PNG");
            }
        }

        private void Log_Message(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{DateTime.Now.ToString()}][{e.Source}] {e.Message}");
        }
    }
}
