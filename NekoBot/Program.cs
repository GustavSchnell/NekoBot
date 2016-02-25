using DiscordSharp;
using NekoBot.Services;
using System;
using System.Threading;

namespace NekoBot
{
    public class Program
    {
        private static DiscordClient client;

        public static void Main(string[] args)
        {
            try
            {
                if (args.Length != 2)
                {
                    throw new ArgumentException("Please try again with the following arguments:\r\n   Email\r\n   Password");
                }

                client = new DiscordClient();
                client.ClientPrivateInformation.email = args[0];
                client.ClientPrivateInformation.password = args[1];
                client.MessageReceived += MessageService.MessageReceived;
                client.SendLoginRequest();
                Thread t = new Thread(client.Connect);
                t.Start();
                Console.WriteLine("Press any key to Exit.");
                Console.ReadKey();
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                Console.ReadKey();
            }
        }
    }
}
