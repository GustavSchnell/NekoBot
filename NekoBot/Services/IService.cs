using Discord;
using System.Collections.Generic;

namespace NekoBot.Services
{
    public interface ICommonService
    {
        string Name { get; }

        void Connect(DiscordClient client);

        List<string> ComamndsHelp { get; }
    }
}
