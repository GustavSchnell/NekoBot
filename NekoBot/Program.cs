using DiscordSharp;
using NekoBot.Services;
using Newtonsoft.Json;
using System;
using System.IO;
using System.Threading;

namespace NekoBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Config config = LoadConfig();
                DiscordClient client = new DiscordClient();
                Login(client, config);

                Console.WriteLine("Press any key to Exit.");
                Console.ReadKey();
                client.Logout();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
            }
        }

        private static Config LoadConfig()
        {
            try
            {

                return JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void Login(DiscordClient client, Config config)
        {
            client.ClientPrivateInformation.email = config.Email;
            client.ClientPrivateInformation.password = config.Password;
            client.MessageReceived += MessageService.MessageReceived;
            client.SendLoginRequest();
            Thread t = new Thread(client.Connect);
            t.Start();
        }
    }
}
