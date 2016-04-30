using DiscordSharp.Events;

namespace Miki.Core.Command.Debug
{
    internal class Whoisserver : Command
    {
        public override void Initialize()
        {
            id = "channelid";
            appearInHelp = false;
            devOnly = true;
            parameterType = ParameterType.NO;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            e.Channel.SendMessage(e.Channel.ID);
            e.Channel.SendMessage(e.Channel.Parent.ID);
            base.PlayCommand(e);
        }
    }
}