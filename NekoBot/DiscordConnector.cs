using Discord;
using NekoBot.Services;
using System;
using System.Collections.Generic;
using System.Linq;

namespace NekoBot
{
    public class DiscordConnector
    {
        private List<ICommonService> services;
        private DiscordClient client;
        private Config config;

        public DiscordConnector(Config config, List<ICommonService> services)
        {
            this.services = services;
            this.config = config;
            client = new DiscordClient();
            client.Log.Message += Log_Message;
            client.MessageReceived += Client_MessageReceived;
        }

        public void Connect()
        {
            client.Log.Info("NekoBot", "Loading Plugins...");

            foreach (var plugin in services)
            {
                plugin.Connect(client);
                client.Log.Info("NekoBot", $"{plugin.Name} loaded");
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
                e.Channel.SendMessage(string.Join(Environment.NewLine, services.Select(x => x.ComamndsHelp).ToList()));
            }
        }

        private void Log_Message(object sender, LogMessageEventArgs e)
        {
            Console.WriteLine($"[{DateTime.Now.ToString()}][{e.Source}] {e.Message}");
        }
    }
}
