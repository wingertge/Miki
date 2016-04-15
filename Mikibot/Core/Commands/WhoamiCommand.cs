using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class WhoamiCommand : Command
    {
        public override void Initialize()
        {
            id = "whoami";
            isPublic = true;
            description = "shows your userID";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            e.Channel.SendMessage(e.Author.ID);
            base.PlayCommand(e);
        }
    }
}
