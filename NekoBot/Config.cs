using System.Collections.Generic;

namespace NekoBot
{
    public class Config
    {
        public Config(string email, string password, List<string> adminCommandUserRoles, List<string> listenOnChannels)
        {
            Email = email;
            Password = password;
            ListenOnChannels = listenOnChannels;
            AdminCommandUserRoles = adminCommandUserRoles;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public List<string> AdminCommandUserRoles { get; private set; }

        public List<string> ListenOnChannels { get; private set; }
    }
}
