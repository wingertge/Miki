using DiscordSharp.Events;
using Miki.Core.Debug;
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

        /// <summary>
        /// This class checks all the commands that are initialized
        /// </summary>
        public void RecieveMessage()
        {
            if (e.MessageText.StartsWith(">"))
            {
                if (!e.Author.IsBot)
                {
                    for (int i = 0; i < commands.Count; i++)
                    {
                        commands[i].CheckCommand(e);
                    }
                }
            }
        }
    }
}
