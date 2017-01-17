using Newtonsoft.Json;
using System;
using System.Net;

namespace NekoBot.Services
{
    public class NekoService
    {
        private const string RandomNekoUrl = "http://random.cat/meow";

        public static string GetRandomNeko()
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
                    Console.WriteLine(ex.Message);
                }
            }

            return randomNeko.Url;
        }
    }
}
