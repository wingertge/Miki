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
            int memsize = 0;
            PerformanceCounter PC = new PerformanceCounter();
            PC.CategoryName = "Process";
            PC.CounterName = "Working Set - Private";
            PC.InstanceName = proc.ProcessName;
            memsize = Convert.ToInt32(PC.NextValue()) / (int)(1024) / (int)1024;
            PC.Close();
            PC.Dispose();
            int members = 0;
            for (int i = 0; i < Discord.client.GetServersList().Count; i++)
            {
                members += Discord.client.GetServersList()[i].Members.Count;
            }
            e.Channel.SendMessage("running on **" + Discord.client.GetServersList().Count + " servers**\nusing **" + memsize + "MB** ram\nused by **" + members + " users**");
        }
    }
}
