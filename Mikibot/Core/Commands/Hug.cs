using DiscordSharp.Events;

namespace Miki.Core.Commands
{
    class Hug:Command
    {
        public override void Initialize()
        {
            id = "hug";
            appearInHelp = true;
            description = "Gives you a hug :)";
            parameterType = ParameterType.BOTH;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            if (e.MessageText.Split(' ').Length > 1)
            {
                e.Channel.SendMessage("<@" + e.Author.ID + "> hugs " + e.MessageText.Split(' ')[1]);
                return;
            }
            e.Channel.SendMessage("Miki hugs " + "<@" + e.Author.ID + ">");
            base.PlayCommand(e);
        }
    }
}