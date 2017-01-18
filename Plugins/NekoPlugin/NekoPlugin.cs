using Discord;
using Newtonsoft.Json;
using PluginContracts;
using System;
using System.Net;

namespace NekoPlugin
{
    public class NekoPlugin : IPlugin
    {
        public string Name => nameof(NekoPlugin);

        private const string RandomNekoUrl = "http://random.cat/meow";
        private DiscordClient client;

        public void Connect(DiscordClient client)
        {
            this.client = client;
            client.MessageReceived += Client_MessageReceived;
        }

        private void Client_MessageReceived(object sender, MessageEventArgs e)
        {
            if (!e.Message.IsAuthor && e.Message.Text.StartsWith("/cat"))
            {
                e.Channel.SendMessage(GetRandomNeko());
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
