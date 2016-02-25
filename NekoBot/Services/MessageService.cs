using DiscordSharp.Events;
using System;

namespace NekoBot.Services
{
    public class MessageService
    {
        public static void MessageReceived(object sender, DiscordMessageEventArgs e)
        {
            if (e.message_text.Equals("/help"))
            {
                e.Channel.SendMessage("Meow Meow!");
                e.Channel.SendMessage("Visit https://github.com/dreanor/NekoBot to report bugs.");
                e.Channel.SendMessage("Type '/cat' for a random cat.");
            }

            if (e.message_text.Equals("/cat"))
            {
                string url = NekoService.GetRandomNeko();
                if (!string.IsNullOrWhiteSpace(url))
                {
                    e.Channel.SendMessage(url);
                    Console.WriteLine(url);
                }
            }
        }
    }
}
