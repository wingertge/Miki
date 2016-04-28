using System;
using System.Diagnostics;

namespace Miki.Core.Commands
{
    public class Statistics : Command
    {
        public override void Initialize()
        {
            id = "stats";
            appearInHelp = true;
            description = "Returns the bot's debug statistics";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordSharp.Events.DiscordMessageEventArgs e)
        {
            Process proc = Process.GetCurrentProcess();
            long memsize = proc.PrivateMemorySize64;
            int members = 0;
            for (int i = 0; i < Discord.client.GetServersList().Count; i++)
            {
                members += Discord.client.GetServersList()[i].Members.Count;
            }
            int threads = proc.Threads.Count;
            e.Channel.SendMessage(
                FormatToStats("Servers", Discord.client.GetServersList().Count.ToString()) +
                FormatToStats("Users", members.ToString()) +
                FormatToStats("Ram", (memsize / 1024 / 1024).ToString() + "MB") +
                FormatToStats("Threads", threads.ToString())
                );
        }

        string FormatToStats(string text, string variable)
        {
            return "`" + text + GetSpace(9 - text.Length) + "` **" + variable + "**\n";
        }
    }
}
