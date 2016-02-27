using DiscordSharp;
using DiscordSharp.Events;
using DiscordSharp.Objects;

namespace NekoBot.Commands
{
    public class AdminCommands
    {
        private DiscordClient client;

        public AdminCommands(DiscordClient client)
        {
            this.client = client;
        }

        public void HandleCommands(DiscordMessageEventArgs e)
        {
            switch (e.message_text)
            {
                case "/clear":
                    ClearCmd(e.Channel);
                    break;
                default:
                    break;
            }
        }

        private void ClearCmd(DiscordChannel channel)
        {
            client.DeleteMultipleMessagesInChannel(channel, int.MaxValue);
        }
    }
}
