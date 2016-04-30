using DiscordSharp.Events;

namespace Miki.Core.Command.Objects
{
    internal class Poke : Command
    {
        public override void Initialize()
        {
            id = "poke";
            appearInHelp = true;
            description = "Give a poke or receive a poke";
            parameterType = ParameterType.BOTH;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            if (e.MessageText.Split(' ').Length > 1)
            {
                e.Channel.SendMessage("<@" + e.Author.ID + "> pokes " + e.MessageText.Split(' ')[1]);
                return;
            }
            e.Channel.SendMessage("Miki pokes " + "<@" + e.Author.ID + ">");
            base.PlayCommand(e);
        }
    }
}