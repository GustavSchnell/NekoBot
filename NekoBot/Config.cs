namespace NekoBot
{
    public class Config
    {
        public Config(string botAccountToken)
        {
            BotAccountToken = botAccountToken;
        }

        public string BotAccountToken { get; private set; }
    }
}
