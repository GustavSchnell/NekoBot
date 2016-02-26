using DiscordSharp;
using DiscordSharp.Events;
using DiscordSharp.Objects;
using NekoBot.Commands;

namespace NekoBot.Services
{
    public class MessageService
    {
        private DiscordClient client;
        private string channelName;
        private bool listenOnAllChannels;

        public MessageService(DiscordClient client, string channelName, bool listenOnAllChannels)
        {
            this.client = client;
            this.channelName = channelName;
            this.listenOnAllChannels = listenOnAllChannels;
        }

        public void MessageReceived(object sender, DiscordMessageEventArgs e)
        {
            if (!listenOnAllChannels && !e.Channel.Name.Equals(channelName.ToLower()))
            {
                return;
            }

            DiscordChannel channel = e.Channel;
            switch (e.message_text)
            {
                case "/help":
                    UserCommands.HelpCmd(channel);
                    break;
                case "/cat":
                    UserCommands.CatCmd(channel);
                    break;
                case "/clear":
                    AdminCommands.ClearCmd(client, channel);
                    break;
                case "/music":
                    UserCommands.MusicCmd(channel);
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
                UserCommands.HelpCmd(channel);
            }
        }
    }
}
