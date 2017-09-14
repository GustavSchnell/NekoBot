using System;
using Discord;
using PluginContracts;
using System.Collections.Generic;
using Newtonsoft.Json;
using System.IO;

namespace MagicBallPlugin
{
    public class MagicBallPlugin : IPlugin
    {
        public string Name => nameof(MagicBallPlugin);

        public List<string> ComamndsHelp => new List<string>
        {
            "/8ball - Ask your question to the magic ball."
        };

        private DiscordClient client;
        private List<string> answers;

        public void Connect(DiscordClient client)
        {
            this.client = client;
            client.MessageReceived += Client_MessageReceived;
            answers = LoadAnwsers();
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message.Text.StartsWith("/8ball"))
            {
                e.Channel.SendMessage(GetRandomAnwser());
            }
        }
      
        private string GetRandomAnwser()
        {
            Random random = new Random();
            int rnd = random.Next(0, answers.Count - 1);
            return answers[rnd];
        }

        private List<string> LoadAnwsers()
        {
            const string configName = "MagicBallAnwsers.json";

            if (!File.Exists(configName))
            {
                File.WriteAllText(configName, JsonConvert.SerializeObject(new Answer(), Formatting.Indented));
            }
            return JsonConvert.DeserializeObject<Answer>(File.ReadAllText(configName)).Answers;
        }
    }
}
