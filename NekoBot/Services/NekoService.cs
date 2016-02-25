using Newtonsoft.Json;
using System;
using System.Net;

namespace NekoBot.Services
{
    public class NekoService
    {
        public static string GetRandomNeko()
        {
            RandomNeko randomNeko = null;
           
            using (WebClient webClient = new WebClient())
            {
                try
                {
                    string json = webClient.DownloadString("http://random.cat/meow");
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
