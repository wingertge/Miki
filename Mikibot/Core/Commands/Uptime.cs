using DiscordSharp.Events;
using System;

namespace Miki.Core.Commands
{
    class Uptime : Command
    {
        public override void Initialize()
        {
            id = "uptime";
            appearInHelp = true;
            description = "shows the bot's time without crashing inbetween.";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            TimeSpan t = DateTime.Now.Subtract(Discord.timeSinceReset);
            e.Channel.SendMessage(t.ToString());
        }
    }
}
