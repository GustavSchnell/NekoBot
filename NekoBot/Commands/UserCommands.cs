using DiscordSharp.Objects;
using NekoBot.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NekoBot.Commands
{
    public class UserCommands
    {
        private Regex translateCmd = new Regex(@"[/]translate '.*.' [a-z]{2}");

        public UserCommands()
        {
            Commands = new List<string> { "/help", "/cat", "/music", "/translate" };
        }

        public List<string> Commands { get; private set; }

        public void HandleCommands(string message, DiscordChannel channel)
        {
            if (translateCmd.IsMatch(message))
            {
                TranslateCmd(message, channel);
                return;
            }

            switch (message)
            {
                case "/cat":
                    CatCmd(channel);
                    return;
                case "/music":
                    MusicCmd(channel);
                    return;
                default:
                    break;
            }
        }

        private static void TranslateCmd(string message, DiscordChannel channel)
        {
            string[] splittedCmd = message.Split('\'');
            string text = splittedCmd[1].Trim();
            string language = splittedCmd[2].Trim();

            channel.SendMessage(string.Format("{0} translates to {1} in {2}",
                StringHelper.Bold(text),
                StringHelper.Bold(TranslateService.Translate(text, language)),
                StringHelper.Bold(language)));
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
    }
}
