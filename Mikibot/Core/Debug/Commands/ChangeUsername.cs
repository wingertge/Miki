using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;

namespace Miki.Core.Debug.Commands
{
    class ChangeUsername:Command
    {
        public override void Initialize()
        {
            id = "name";
            appearInHelp = false;
            devOnly = true;
            description = "";

            parameterType = ParameterType.YES;
            usage = new string[] { "name" };

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            Discord.client.ChangeClientInformation(new DiscordSharp.Objects.DiscordUserInformation() { Username = e.MessageText.Split(' ')[1], Avatar=Discord.client.ClientPrivateInformation.Avatar});
            base.PlayCommand(e);
        }
    }
}
