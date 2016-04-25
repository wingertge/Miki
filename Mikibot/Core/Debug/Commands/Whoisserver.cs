using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class Whoisserver : Command
    {
        public override void Initialize()
        {
            id = "channelid";
            appearInHelp = false;
            devOnly = true;
            parameterType = ParameterType.NO;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            e.Channel.SendMessage(e.Channel.ID);
            e.Channel.SendMessage(e.Channel.Parent.ID);
            base.PlayCommand(e);
        }
    }
}
