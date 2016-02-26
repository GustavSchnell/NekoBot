namespace NekoBot
{
    public class Config
    {
        public Config(string email, string password, string channelName, bool listenOnAllChannels)
        {
            Email = email;
            Password = password;
            ChannelName = channelName;
            ListenOnAllChannels = listenOnAllChannels;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string ChannelName { get; private set; }

        public bool ListenOnAllChannels { get; private set; }
    }
}
