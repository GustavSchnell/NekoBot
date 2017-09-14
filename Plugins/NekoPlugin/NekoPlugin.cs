using Discord;
using Newtonsoft.Json;
using PluginContracts;
using System;
using System.Net;
using System.Collections.Generic;

namespace NekoPlugin
{
    public class NekoPlugin : IPlugin
    {
        public string Name => nameof(NekoPlugin);

        public List<string> ComamndsHelp => new List<string>
        {
            "/cat - Posts random cats."
        };

        private const string RandomNekoUrl = "http://random.cat/meow";
        private DiscordClient client;

        public void Connect(DiscordClient client)
        {
            this.client = client;
            client.MessageReceived += Client_MessageReceived;
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            if (e.Message.Text.Equals("/cat"))
            {
                e.Channel.SendMessage(GetRandomNeko());
                e.Message.Delete();
            }
        }

        private string GetRandomNeko()
        {
            RandomNeko randomNeko = null;

            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string json = webClient.DownloadString(RandomNekoUrl);
                    randomNeko = JsonConvert.DeserializeObject<RandomNeko>(json);
                }
                catch (Exception ex)
                {
                    client.Log.Error(Name, ex);
                }
            }

            return randomNeko.Url;
        }
    }
}
