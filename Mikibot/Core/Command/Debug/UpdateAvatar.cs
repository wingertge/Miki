using DiscordSharp.Events;

namespace Miki.Core.Command.Debug
{
    internal class UpdateAvatar : Command
    {
        public override void Initialize()
        {
            id = "updateavatar";
            parameterType = ParameterType.NO;

            base.Initialize();
        }

        public override void CheckCommand(DiscordMessageEventArgs e)
        {
            Discord.config.UpdateAvatar();
            base.CheckCommand(e);
        }
    }
}