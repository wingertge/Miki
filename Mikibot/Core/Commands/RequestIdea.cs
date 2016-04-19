using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;
using DiscordSharp.Objects;

namespace Miki.Core.Commands
{
    class RequestIdea : Command
    {
        public DiscordChannel Channel;

        public override void Initialize()
        {
            id = "request";
            appearInHelp = true;
            description = "request an idea for future updates";

            hasParameters = true;
            expandedDescription = "usage: <command_name, explanation>\nrequest an idea to the developers via Miki. We will recieve all ideas in our main server.";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {


            base.PlayCommand(e);
        }
    }
}
