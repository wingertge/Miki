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
            e.Channel.SendMessage("__** Miki " + Global.VersionNumber + " **__\nMade by: **Veld#5128**\nLibraries used: **DiscordSharp**, **IMDbNet**, **Json.NET**\nWebsite: http://velddev.github.io/miki");
            Discord.account.GetAccountFromID(e.Author.ID).achievements.GetAchievement("informed").isGoingToAchieve = true;
            base.PlayCommand(e);
        }
    }
}
