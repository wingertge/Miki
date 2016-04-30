using DiscordSharp.Events;

namespace Miki.Core.Command.Objects
{
    internal class Pat : Command
    {
        public override void Initialize()
        {
            id = "pat";
            appearInHelp = true;
            description = "Give a pat or receive a pat";
            parameterType = ParameterType.BOTH;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            if (e.MessageText.Split(' ').Length > 1)
            {
                e.Channel.SendMessage("<@" + e.Author.ID + "> pats " + e.MessageText.Split(' ')[1]);
                return;
            }
            e.Channel.SendMessage("Miki pats " + "<@" + e.Author.ID + ">");
            base.PlayCommand(e);
        }
    }
}