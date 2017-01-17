namespace PluginContracts
{
    public interface IPlugin
    {
        string Name { get; }

        string CreateMessage();
    }
}
