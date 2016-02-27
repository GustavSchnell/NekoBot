using System;
using DiscordSharp;
using DiscordSharp.Events;
using DiscordSharp.Objects;
using NekoBot.Commands;
using System.Linq;

namespace NekoBot.Services
{
    public class MessageService
    {
        private DiscordClient client;
        private Config config;
        private AdminCommands adminCommands;

        public MessageService(DiscordClient client, Config config)
        {
            this.config = config;
            this.client = client;
            adminCommands = new AdminCommands(client);
        }

        public void PrivateMessageReceived(object sender, DiscordPrivateMessageEventArgs e)
        {
            e.Channel.recipient.SendMessage("Sorry! Private message commands don't work. Please talk to me in a public channel.");
        }

        public void MessageReceived(object sender, DiscordMessageEventArgs e)
        {
            DiscordChannel channel = e.Channel;

            if (!config.ListenOnAllChannels && !channel.Name.Equals(config.ChannelName.ToLower()))
            {
                return;
            }

            if (e.author.Roles.Any(x => config.AdminCommandUserRoles.Any(y => x.name.ToLower().Equals(y.ToLower()))))
            {
                adminCommands.HandleCommands(e);
            }

            switch (e.message_text)
            {
                case "/help":
                    UserCommands.HelpCmd(channel);
                    break;
                case "/cat":
                    UserCommands.CatCmd(channel);
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
            DiscordChannel channel = client.GetChannelByName(config.ChannelName);
            if (channel != null)
            {
                UserCommands.HelpCmd(channel);
            }
        }
    }
}
