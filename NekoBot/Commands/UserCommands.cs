using DiscordSharp.Objects;
using NekoBot.Services;
using System;
using System.Text;

namespace NekoBot.Commands
{
    public class UserCommands
    {
        public static void MusicCmd(DiscordChannel channel)
        {
            channel.SendMessage("http://nigge.rs/");
        }

        public static void CatCmd(DiscordChannel channel)
        {
            string url = NekoService.GetRandomNeko();
            if (string.IsNullOrWhiteSpace(url))
            {
                channel.SendMessage("Ups! Something went wrong. Please try again.");
                return;
            }

            channel.SendMessage(url);
        }

        public static void HelpCmd(DiscordChannel channel)
        {
            string welcome = "Meow Meow!";
            string catCmd = "Type '/cat' for a random cat.";
            string clearCmd = "Type '/clear' to clear the current channel chat.";
            string musicCmd = "Type '/music' for a random meme music link.";

            string message = BuildMessage(welcome, catCmd, clearCmd, musicCmd);

            channel.SendMessage(message);
        }

        private static string BuildMessage(params string[] messages)
        {
            StringBuilder stringBuilder = new StringBuilder();

            foreach (string message in messages)
            {
                stringBuilder.Append(message);
                stringBuilder.Append(Environment.NewLine);
            }

            return stringBuilder.ToString();
        }
    }
}
