using DiscordSharp.Events;

namespace Miki.Core.Command.Objects
{
    internal class BlacklistServer : Command
    {
        public override void Initialize()
        {
            id = "blacklist";
            appearInHelp = false;
            description = "blacklist server";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            if (e.Author.ID == "121919449996460033")
            {
                Discord.blacklist.blacklist.Add(e.Channel.ID);
                Discord.config.SaveBlacklist();
                e.Channel.SendMessage("I won't bother you no more.");
            }
            base.PlayCommand(e);
        }
    }
}