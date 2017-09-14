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
                client.SetGame("Say /help");
            });

            
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message.Text.Equals("/help"))
            {
                e.Channel.SendMessage("The following commands are available:");
                foreach (var plugin in availablePlugins)
                {
                    foreach (var command in plugin.Value.ComamndsHelp)
                    {
                        e.Channel.SendMessage(command);
                    }
                }
            }
        }

        private void Log_Message(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{DateTime.Now.ToString()}][{e.Source}] {e.Message}");
        }
    }
}
