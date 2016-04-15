using DiscordSharp.Events;
using Miki.Extensions.IMDb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Miki.Core
{
    public class ChannelMessage
    {
        DiscordMessageEventArgs e;
        public static List<Command> commands = new List<Command>();

        public ChannelMessage(DiscordMessageEventArgs e)
        {
            this.e = e;
        }

        public void RecieveMessage()
        {
            if (e.MessageText.StartsWith(">"))
            {
                for (int i = 0; i < commands.Count; i++)
                {
                    commands[i].CheckCommand(e);
                }
            }
        }
    }
}
