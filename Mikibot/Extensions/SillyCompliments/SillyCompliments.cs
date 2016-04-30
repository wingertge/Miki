using DiscordSharp.Events;
using Miki.Core;
using Miki.Core.Command;
using System;

namespace Miki.Extensions.SillyCompliments
{
    internal class SillyCompliments_Core : Command
    {
        private Random r = new Random();

        private string[] I_LIKE = new string[]
        {
            "I like ",
            "I love ",
            "I admire ",
            "I really enjoy ",
            "For some reason i like ",
        };

        private string[] BODY_PART = new string[]
        {
            "the lower part of your lips",
            "the smallest toe on your left foot",
            "the smallest toe on your right foot",
            "the second eyelash from your left eye",
            "the lower part of your chin",
            "your creepy finger in your left hand",
            "your cute smile",
            "those dazzling eyes of yours",
            "your creepy finger in your right hand",
            "the special angles your elbows makes",
            "the dimples on your cheeks"
        };

        private string[] SUFFIX = new string[]
        {
            " alot",
            " a bit",
            " quite a bit",
            " a lot, is that weird?",
        };

        public override void Initialize()
        {
            id = "compliment";
            appearInHelp = true;
            description = "Gives you a compliment :)";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            base.PlayCommand(e);
            e.Channel.SendMessage(I_LIKE[r.Next(0, I_LIKE.Length)] + BODY_PART[r.Next(0, BODY_PART.Length)] + SUFFIX[r.Next(0, SUFFIX.Length)]);
        }
    }
}