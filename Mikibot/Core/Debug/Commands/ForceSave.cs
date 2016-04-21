using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class ForceSave : Command
    {
        public override void Initialize()
        {
            id = "saveprofiles";
            appearInHelp = false;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            Discord.account.SaveAccountsOnCommand();
            e.Channel.SendMessage("Your data is safe, Senpai!");
            base.PlayCommand(e);
        }
    }
}
