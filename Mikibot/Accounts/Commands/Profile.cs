using DiscordSharp.Events;
using DiscordSharp.Objects;
using Miki.Core;
using Miki.Core.Command;

namespace Miki.Accounts.Commands
{
    internal class Profile : Command
    {
        public override void Initialize()
        {
            id = "profile";
            appearInHelp = true;
            description = "shows your profile";

            parameterType = ParameterType.BOTH;

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            base.PlayCommand(e);
            if (message.StartsWith("profile "))
            {
                DiscordMemberHandler m = new DiscordMemberHandler();
                DiscordMember member = m.GetMemberFromLink(e.Channel, message);
                Discord.account.AddAccount(member, e.Channel);
                e.Channel.SendMessage(Discord.account.GetProfile(member.ID));
            }
            else if (Discord.account.GetProfile(e.Author.ID) != "")
            {
                e.Channel.SendMessage(Discord.account.GetProfile(e.Author.ID));
            }
        }
    }
}