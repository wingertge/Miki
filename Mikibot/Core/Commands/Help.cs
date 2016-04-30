using DiscordSharp.Events;

namespace Miki.Core.Command.Objects
{
    internal class HelpCommand : Command
    {
        public override void Initialize()
        {
            id = "help";
            appearInHelp = false;
            description = "help";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            string output = "";
            for (int i = 0; i < CommandManager.commands.Count; i++)
            {
                output += CommandManager.commands[i].GetHelpLine();
            }
            e.Channel.SendMessage(output);
            base.PlayCommand(e);
        }
    }
}