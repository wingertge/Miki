using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class UpdateAvatar : Command
    {
        public override void Initialize()
        {
            id = "updateavatar";
            parameterType = ParameterType.NO;

            base.Initialize();
        }

        public override void CheckCommand(DiscordMessageEventArgs e)
        {
            Discord.config.UpdateAvatar();
            base.CheckCommand(e);
        }
    }
}
