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
            string musicCmd = "Type '/music' for a random meme music link.";

            string message = StringHelper.BuildMessage(welcome, catCmd, musicCmd);

            channel.SendMessage(message);
        }
    }
}
