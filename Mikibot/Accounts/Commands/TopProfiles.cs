using DiscordSharp.Events;
using Miki.Core;
using Miki.Core.Command;

namespace Miki.Accounts.Commands
{
    internal class TopProfiles : Command
    {
        public override void Initialize()
        {
            id = "leaderboards";
            appearInHelp = true;
            description = "check the top 10 highest ranking profiles";
            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            base.PlayCommand(e);
            string output = "";
            Account[] a = Discord.account.GetAccountLeaderboards(false, e.Channel.Parent);
            output += ":crown:: `" + a[0].GetMember(a[0].memberID).Username + " (" + a[0].profile.Experience + ")`\n";
            for (int i = 1; i < a.Length; i++)
            {
                if (a[i] != null)
                {
                    output += i + 1 + " : `" + a[i].GetMember(a[i].memberID).Username + " (" + a[i].profile.Experience + ")`\n";
                }
            }
            e.Channel.SendMessage(output);
        }
    }
}