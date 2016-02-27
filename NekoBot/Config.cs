using System.Collections.Generic;

namespace NekoBot
{
    public class Config
    {
        public Config(string email, string password, string channelName, bool listenOnAllChannels, List<string> adminCommandUserRoles)
        {
            Email = email;
            Password = password;
            ChannelName = channelName;
            ListenOnAllChannels = listenOnAllChannels;
            AdminCommandUserRoles = adminCommandUserRoles;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string ChannelName { get; private set; }

        public bool ListenOnAllChannels { get; private set; }

        public List<string> AdminCommandUserRoles { get; private set; }
    }
}
