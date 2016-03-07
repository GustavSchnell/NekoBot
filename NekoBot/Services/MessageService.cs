using DiscordSharp;
using DiscordSharp.Events;
using DiscordSharp.Objects;
using NekoBot.Commands;
using System;
using System.Linq;
using System.Threading;

namespace NekoBot.Services
{
    public class MessageService
    {
        private DiscordClient client;
        private Config config;
        private AdminCommands adminCommands;
        private UserCommands userCommands;
        private Semaphore apiCallsBlocker;

        public MessageService(DiscordClient client, Config config)
        {
            this.config = config;
            this.client = client;
            adminCommands = new AdminCommands(client);
            adminCommands.ConfigReload += (o, e) => this.config = (Config)o;
            userCommands = new UserCommands();
            apiCallsBlocker = new Semaphore(5, 5);
        }

        public void PrivateMessageReceived(object sender, DiscordPrivateMessageEventArgs e)
        {
            apiCallsBlocker.WaitOne();
            e.Channel.recipient.SendMessage("Sorry! Private message commands don't work. Please talk to me in a public channel.");
            apiCallsBlocker.Release(1);
        }

        public void MessageReceived(object sender, DiscordMessageEventArgs e)
        {
            try
            {
                DiscordChannel channel = e.Channel;
                string message = e.message_text;

                if (!AllowedToListenTo(channel))
                {
                    return;
                }

                Console.WriteLine(e.message_text);

                apiCallsBlocker.WaitOne();
                if (AllowedToExecuteAdminCommands(e))
                {
                    adminCommands.HandleCommands(e);
                }

                if (userCommands.Commands.Any(x => message.StartsWith(x)))
                {
                    userCommands.HandleCommands(message, channel);
                }
                apiCallsBlocker.Release(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(string.Format("Latest message send: '{0}' by '{1}'{2}{3}", e.message_text, e.author, Environment.NewLine, ex.Message));
            }
        }

        private bool AllowedToExecuteAdminCommands(DiscordMessageEventArgs e)
        {
            return e.author.Roles.Any(x => config.AdminCommandUserRoles.Any(y => y.Equals(x.name, StringComparison.OrdinalIgnoreCase)));
        }

        private bool AllowedToListenTo(DiscordChannel channel)
        {
            return config.ListenOnChannels.Any(x => x.Equals(channel.Name, StringComparison.OrdinalIgnoreCase));
        }
    }
}
