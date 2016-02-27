using DiscordSharp;
using DiscordSharp.Events;
using DiscordSharp.Objects;
using System;
using System.Text.RegularExpressions;

namespace NekoBot.Commands
{
    public class AdminCommands
    {
        private DiscordClient client;
        private Regex deleteCommand = new Regex(@"[/]delete.\d+");

        public AdminCommands(DiscordClient client)
        {
            this.client = client;
        }

        public void HandleCommands(DiscordMessageEventArgs e)
        {
            string message = e.message_text;

            if (deleteCommand.IsMatch(message))
            {
                DeleteMessagesCmd(e, message);
                return;
            }

            switch (message)
            {
                case "/clear":
                    ClearCmd(e.Channel);
                    return;
                default:
                    break;
            }
        }

        private void DeleteMessagesCmd(DiscordMessageEventArgs e, string message)
        {
            string[] splittedCommand = message.Split(' ');

            int result = 0;
            int.TryParse(splittedCommand[1], out result);

            client.DeleteMultipleMessagesInChannel(e.Channel, result);
        }

        private void ClearCmd(DiscordChannel channel)
        {
            client.DeleteMultipleMessagesInChannel(channel, int.MaxValue);
        }
    }
}
