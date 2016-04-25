using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class HelpCommand:Command
    {
        public override void Initialize()
        {
            id = "help";
            appearInHelp = false;
            description = "help";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            string output = "";
            for (int i = 0; i < ChannelMessage.commands.Count; i++)
            {
                output += ChannelMessage.commands[i].GetHelpLine();
            }
            e.Channel.SendMessage(output);
            base.PlayCommand(e);
        }
    }
}
