using DiscordSharp.Objects;
using NekoBot.Services;
using System;
using System.Collections.Generic;
using System.Text;

namespace NekoBot.Commands
{
    public class UserCommands
    {
        public UserCommands()
        {
            Commands = new List<string> { "/help", "/cat", "/music", "/translate" };
        }

        public List<string> Commands { get; private set; }

        public void HandleCommands(string message, DiscordChannel channel)
        {
            // /translate "du bist doof",de,e 

            if (message.StartsWith("/translate"))
            {
                var b = message.Split('\'');

                if (b.Length != 3)
                {
                    channel.SendMessage("Wrong command! The correct syntax is: /translate 'text' de");
                }
          

                channel.SendMessage(string.Format("'{0}' translates to '{1}' in '{2}'",
                b[1].Trim(),
                TranslateService.Translate(b[1].Trim(), b[2].Trim()),
                b[2].Trim()));
                return;
            }

            switch (message)
            {
                case "/help":
                    HelpCmd(channel);
                    break;
                case "/cat":
                    CatCmd(channel);
                    break;
                case "/music":
                    MusicCmd(channel);
                    break;
                default:
                    break;
            }
        }

        private void MusicCmd(DiscordChannel channel)
        {
            channel.SendMessage("http://nigge.rs/");
        }

        private void CatCmd(DiscordChannel channel)
        {
            string url = NekoService.GetRandomNeko();
            if (string.IsNullOrWhiteSpace(url))
            {
                channel.SendMessage("Ups! Something went wrong. Please try again.");
                return;
            }

            channel.SendMessage(url);
        }

        private void HelpCmd(DiscordChannel channel)
        {
            string welcome = "Meow Meow!";
            string catCmd = "Type '/cat' for a random cat.";
            string musicCmd = "Type '/music' for random meme music.";

            string message = StringHelper.BuildMessage(welcome, catCmd, musicCmd);

            channel.SendMessage(message);
        }
    }
}
