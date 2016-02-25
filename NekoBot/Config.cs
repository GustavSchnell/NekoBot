namespace NekoBot
{
    public class Config
    {
        public Config(string email, string password, string channelName)
        {
            Email = email;
            Password = password;
            ChannelName = channelName;
        }

        public string Email { get; private set; }

        public string Password { get; private set; }

        public string ChannelName { get; private set; }
    }
}
