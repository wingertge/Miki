using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;
using DiscordSharp.Objects;

namespace Miki.Core.Debug.Commands
{
    class Ping:Command
    {
        public override void Initialize()
        {
            id = "ping";
            appearInHelp = true;
            description = "pong";
            parameterType = ParameterType.NO;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            DiscordMessage m = e.Channel.SendMessage("Pong! ");
            Discord.client.EditMessage(m.ID, "Pong! " + (m.timestamp.TimeOfDay - e.Message.timestamp.TimeOfDay).Milliseconds + "ms", e.Channel);
            base.PlayCommand(e);
        }
    }
}
