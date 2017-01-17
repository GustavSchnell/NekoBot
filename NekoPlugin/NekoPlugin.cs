using PluginContracts;

namespace NekoPlugin
{
    public class NekoPlugin : IPlugin
    {
        public string Name => nameof(NekoPlugin);

        public string CreateMessage()
        {
            return "meow";
        }
    }
}
