using DiscordSharp;
using NekoBot.Services;
using System;
using System.Threading;

namespace NekoBot
{
    public class Program
    {
        public static void Main(string[] args)
        {
            try
            {
                Config config = ConfigLoader.Get();
                DiscordClient client = new DiscordClient();
                MessageService messageService = new MessageService(client, config);

                Login(client, messageService, config);

                Console.WriteLine("Press enter to Exit.");
                Console.ReadLine();
                client.Logout();
            }
            catch (Exception ex)
            {
                Console.WriteLine("Error: " + ex.Message);
                Console.ReadKey();
            }
        }

        private static void Login(DiscordClient client, MessageService messageService, Config config)
        {
            client.ClientPrivateInformation.email = config.Email;
            client.ClientPrivateInformation.password = config.Password;
            client.MessageReceived += messageService.MessageReceived;
            client.PrivateMessageReceived += messageService.PrivateMessageReceived;

#if DEBUG
            client.TextClientDebugMessageReceived += (o, e) => Console.WriteLine(e.message.Message);
#endif

            try
            {
                client.SendLoginRequest();
            }
            catch (Exception)
            {
                throw new InvalidOperationException("Couldn't connect to Discord. Please check your credentials in the config.json file.");
            }

            Thread t = new Thread(client.Connect);
            t.Start();
        }
    }
}
