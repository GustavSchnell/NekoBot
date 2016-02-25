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
                MessageService messageService = new MessageService(client, config.ChannelName);

                Login(client, messageService, config);

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
                if (!File.Exists("config.json"))
                {
                    string json = JsonConvert.SerializeObject(new Config("email", "password", "NekoBot"), Formatting.Indented);
                    File.WriteAllText(@"config.json", json);
                    ThrowCredentialsException();
                }

                return JsonConvert.DeserializeObject<Config>(File.ReadAllText("config.json"));
            }
            catch (Exception)
            {
                throw;
            }
        }

        private static void Login(DiscordClient client, MessageService messageService, Config config)
        {
            client.ClientPrivateInformation.email = config.Email;
            client.ClientPrivateInformation.password = config.Password;
            client.MessageReceived += messageService.MessageReceived;
            client.Connected += messageService.Connected;

            try
            {
                client.SendLoginRequest();
            }
            catch (Exception)
            {
                ThrowCredentialsException();
            }

            Thread t = new Thread(client.Connect);
            t.Start();
        }

        private static void ThrowCredentialsException()
        {
            throw new InvalidOperationException("Couldn't connect to Discord. Please check your credentials in the config.json file.");
        }
    }
}
