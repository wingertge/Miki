using DiscordSharp.Events;

namespace Miki.Core.Command.Debug
{
    internal class Whoami : Command
    {
        public override void Initialize()
        {
            id = "whoami";
            appearInHelp = true;
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