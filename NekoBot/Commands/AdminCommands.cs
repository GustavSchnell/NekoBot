using DiscordSharp;
using DiscordSharp.Objects;

namespace NekoBot.Commands
{
    public class AdminCommands
    {
        public static void ClearCmd(DiscordClient client, DiscordChannel channel)
        {
            client.DeleteMultipleMessagesInChannel(channel, int.MaxValue);
        }
    }
}
