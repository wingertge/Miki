using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class InfoCommand : Command
    {
        public override void Initialize()
        {
            id = "info";
            appearInHelp = true;
            description = "description about the bot itself.";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            e.Channel.SendMessage("**Miki " + Global.VersionText + "**\nMade by: Veld#5128\nUses: DiscordSharp by AXIYUM#6863\n\n Thank you for using miki! ");
            base.PlayCommand(e);
        }
    }
}
