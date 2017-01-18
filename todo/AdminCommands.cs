using DiscordSharp;
using DiscordSharp.Events;
using DiscordSharp.Objects;
using System;
using System.Text.RegularExpressions;

namespace NekoBot.Commands
{
    public class AdminCommands
    {
        public event EventHandler ConfigReload;
        private DiscordClient client;
        private Regex deleteCommand = new Regex(@"[/]delete.\d+");

        public AdminCommands(DiscordClient client)
        {
            this.client = client;
        }

        public bool HandleCommands(DiscordMessageEventArgs e)
        {
            string message = e.MessageText;

            if (deleteCommand.IsMatch(message))
            {
                return DeleteMessagesCmd(e, message);
            }

            switch (message)
            {
                case "/clear":
                    ClearCmd(e.Channel);
                    return true;
                case "/reload":
                    ReloadCmd();
                    return true;
                default:
                    return true;
            }
        }

        private void ReloadCmd()
        {
            ConfigReload(ConfigLoader.Get(), null);
        }

        private bool DeleteMessagesCmd(DiscordMessageEventArgs e, string message)
        {
            string[] splittedCommand = message.Split(' ');

            int result = 0;
            bool parsed = int.TryParse(splittedCommand[1], out result);
            if (parsed)
            {
                client.DeleteMultipleMessagesInChannel(e.Channel, result);
            }
            else
            {
                e.Channel.SendMessage("Wrong command.");
            }
            return parsed;
        }

        private void ClearCmd(DiscordChannel channel)
        {
            client.DeleteMultipleMessagesInChannel(channel, int.MaxValue);
        }
    }
}
