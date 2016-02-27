using System;
using DiscordSharp;
using DiscordSharp.Events;
using DiscordSharp.Objects;
using NekoBot.Commands;
using System.Linq;
using System.Collections.Generic;

namespace NekoBot.Services
{
    public class MessageService
    {
        private DiscordClient client;
        private Config config;
        private AdminCommands adminCommands;
        private UserCommands userCommands;

        public MessageService(DiscordClient client, Config config)
        {
            this.config = config;
            this.client = client;
            adminCommands = new AdminCommands(client);
            userCommands = new UserCommands();
        }

        public void PrivateMessageReceived(object sender, DiscordPrivateMessageEventArgs e)
        {
            e.Channel.recipient.SendMessage("Sorry! Private message commands don't work. Please talk to me in a public channel.");
        }

        public void MessageReceived(object sender, DiscordMessageEventArgs e)
        {
            DiscordChannel channel = e.Channel;
            string message = e.message_text;

            if (!AllowedToListenTo(channel))
            {
                return;
            }

            if (AllowedToExecuteAdminCommands(e))
            {
                adminCommands.HandleCommands(e);
            }

            if (userCommands.Commands.Any(x => message.StartsWith(x)))
            {
                userCommands.HandleCommands(message, channel);
            }
        }

        private bool AllowedToExecuteAdminCommands(DiscordMessageEventArgs e)
        {
            return e.author.Roles.Any(x => config.AdminCommandUserRoles.Any(y => x.name.Equals(y, StringComparison.OrdinalIgnoreCase)));
        }

        private bool AllowedToListenTo(DiscordChannel channel)
        {
            return config.ListenOnChannels.Any(x => x.Equals(channel.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
