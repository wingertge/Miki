using DiscordSharp.Events;
using DiscordSharp.Objects;

namespace Miki.Core.Command.Debug
{
    internal class Ping : Command
    {
        public override void Initialize()
        {
            id = "ping";
            appearInHelp = true;
            description = "pong";
            parameterType = ParameterType.NO;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            DiscordMessage m = e.Channel.SendMessage("Pong! ");
            Discord.client.EditMessage(m.ID, "Pong! " + (m.timestamp.TimeOfDay - e.Message.timestamp.TimeOfDay).Milliseconds + "ms", e.Channel);
            base.PlayCommand(e);
        }
    }
}