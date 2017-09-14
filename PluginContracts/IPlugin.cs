using Discord;
using System.Collections.Generic;

namespace PluginContracts
{
    public interface IPlugin
    {
        string Name { get; }

        void Connect(DiscordClient client);

        List<string> ComamndsHelp { get; }
    }
}
