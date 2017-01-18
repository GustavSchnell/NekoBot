using Discord;

namespace PluginContracts
{
    public interface IPlugin
    {
        string Name { get; }

        void Connect(DiscordClient client);
    }
}
