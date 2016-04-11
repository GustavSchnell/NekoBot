using DiscordSharp.Objects;
using NekoBot.Services;
using System;
using System.Collections.Generic;
using System.Text.RegularExpressions;

namespace NekoBot.Commands
{
    public class UserCommands
    {
        public List<string> Commands { get; private set; }

        public bool HandleCommands(string message, DiscordChannel channel)
        {
            var match = Regex.Match(message, @"[/]translate .*. [a-z]{2}");

            if (match.Success)
            {
                if (match.Length == message.Length)
                {
                    TranslateCmd(message, channel);
                    return true;
                }

                return false;
            }

            switch (message)
            {
                case "/cat":
                    CatCmd(channel);
                    return true;
                case "/music":
                    MusicCmd(channel);
                    return true;
                case "/8ball":
                    channel.SendMessage(EightBallService.Get());
                    return true;
                case "/help":
                    channel.SendMessage("https://raw.githubusercontent.com/dreanor/NekoBot/master/NekoBot/commands.PNG");
                    return true;
                default:
                    return true;
            }
        }

        private static void TranslateCmd(string message, DiscordChannel channel)
        {
            string withoutCmd = message.Remove(0, 10).Trim();
            string language = withoutCmd.Substring(withoutCmd.Length - 2);
            string text = withoutCmd.Remove(withoutCmd.Length - 2).Trim();

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
