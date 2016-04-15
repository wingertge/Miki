using Miki.Core;
using DiscordSharp.Events;

namespace Miki.Accounts.Commands
{
    class Profile : Command
    {
        public override void Initialize()
        {
            id = "profile";
            isPublic = true;
            description = "shows your profile";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            base.PlayCommand(e);
            if (message.StartsWith("profile "))
            {
                DiscordMemberHandler m = new DiscordMemberHandler();
                e.Channel.SendMessage(Discord.account.GetProfile(m.GetMemberFromLink(e.Channel, message)));
            }
            else if (Discord.account.GetProfile(e.Author) != "")
            {
                e.Channel.SendMessage(Discord.account.GetProfile(e.Author));
            }
        }
    }
}
