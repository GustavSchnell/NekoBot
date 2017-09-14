using NekoBot.Services;
using NekoBot.Services.Eightball;
using NekoBot.Services.Neko;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.IO;

namespace NekoBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            new DiscordConnector(LoadConfig(), new List<ICommonService>
            {
                new NekoService(),
                new MagicBallService()
            }).Connect();
        }

        private static Config LoadConfig()
        {
            const string configName = "config.json";

            if (!File.Exists(configName))
            {
                File.WriteAllText(configName, JsonConvert.SerializeObject(new Config(""), Formatting.Indented));
            }
            return JsonConvert.DeserializeObject<Config>(File.ReadAllText(configName));
        }
    }
}
