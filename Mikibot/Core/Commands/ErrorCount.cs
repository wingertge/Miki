using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class ErrorCount :Command
    {
        public override void Initialize()
        {
            id = "errors";
            appearInHelp = false;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            e.Channel.SendMessage("I fucked up " + Discord.errors + " times, senpai");
            base.PlayCommand(e);
        }
    }
}
