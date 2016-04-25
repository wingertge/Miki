using DiscordSharp.Events;
using Miki.Core;
using System;

namespace Miki.Extensions.Hug
{
    class Hug_Discord_Core:Command
    {
        //Random r = new Random();

        String Hug = "_hugs_";

        public override void Initialize()
        {
            id = "hug";
            appearInHelp = true;
            description = "Gives you a hug :)";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            base.PlayCommand(e);
            e.Channel.SendMessage(Hug);
        }
    }
}