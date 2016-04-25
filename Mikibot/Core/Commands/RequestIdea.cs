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

        public override void Initialize()
        {
            id = "request";
            appearInHelp = true;
            description = "request an idea for future updates";

            parameterType = ParameterType.YES;
            expandedDescription = "usage: <explanation>\nrequest an idea to the developer via Miki. We will recieve all ideas in our main server.";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            string idea = message.Substring(8);
            e.Channel.SendMessage("Thank you for your idea, we will check it out, " + e.Author.Username + "-senpai\n__Check our roadmap__ | https://trello.com/b/4Mgl8nBa/miki");
            Discord.client.SendMessageToChannel("__**" + e.Author.Username + "** from **" + e.Channel.Parent.Name + "** has an idea**__\n" + idea, Discord.client.GetServersList().Find(x => "160067691783127041" == x.ID).Channels.Find(x => Global.RequestChannelID == x.ID));
            base.PlayCommand(e);
        }
    }
}
