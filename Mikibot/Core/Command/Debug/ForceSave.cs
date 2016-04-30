using DiscordSharp.Events;

namespace Miki.Core.Command.Debug
{
    internal class ForceSave : Command
    {
        public override void Initialize()
        {
            id = "saveprofiles";
            appearInHelp = false;
            devOnly = true;
            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            Discord.account.SaveAccountsOnCommand();
            e.Channel.SendMessage("Your data is safe, Senpai!");
            base.PlayCommand(e);
        }
    }
}