using DiscordSharp.Events;
using DiscordSharp.Objects;
using System;
using System.Reflection;
using DiscordSharp;
using System.Text;

namespace NekoBot.Services
{
    public class MessageService
    {
        private DiscordClient client;
        private string channelName;

        public MessageService(DiscordClient client, string channelName)
        {
            this.client = client;
            this.channelName = channelName;
        }

        public void MessageReceived(object sender, DiscordMessageEventArgs e)
        {
            if (!e.Channel.Name.Equals(channelName.ToLower()))
            {
                return;
            }

            DiscordChannel channel = e.Channel;
            switch (e.message_text)
            {
                case "/help":
                    HelpCmd(channel);
                    break;
                case "/cat":
                    CatCmd(channel);
                    break;
                default:
                    break;
            }
        }

        public void Connected(object sender, DiscordConnectEventArgs e)
        {
            DiscordChannel channel = client.GetChannelByName(channelName);
            if (channel != null)
            {
                HelpCmd(channel);
            }
        }

        private void CatCmd(DiscordChannel channel)
        {
            string url = NekoService.GetRandomNeko();
            if (string.IsNullOrWhiteSpace(url))
            {
                channel.SendMessage("Ups! Something went wrong. Please try again.");
                return;
            }

            channel.SendMessage(url);
        }

        private void HelpCmd(DiscordChannel channel)
        {
            string welcome = "Meow Meow!";
            string developer = "Visit https://github.com/dreanor/NekoBot to report bugs or request features.";
            string line = "--------------------------------";
            string commands = "Type '/cat' for a random cat.";

            string message = BuildMessage(welcome, developer, line, commands);

            channel.SendMessage(message);
        }

        private string BuildMessage(params string[] messages)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string message in messages)
            {
                stringBuilder.Append(message);
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}
