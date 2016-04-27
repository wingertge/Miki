using DiscordSharp.Objects;
using Miki.Accounts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Core
{
    public class DiscordMemberHandler
    {
        /// <summary>
        /// If you mention someone in discord. It looks weird in code. This turns that data into a Discord ID
        /// </summary>
        /// <param name="channel">Channel where the mention happened</param>
        /// <param name="link">The mention</param>
        /// <param name="offset">Which word it'll be on</param>
        /// <returns>the member's ID</returns>
        public string GetIDFromLink(DiscordChannel channel, string link, int offset = 1)
        {
            string output = link;
            output = link.Split(' ')[offset];
            output = output.Trim(new char[] { '@', '<', '>' });
            return output;
        }

        /// <summary>
        /// Returns the member that gets mentioned. more info about this on GetIDFromLink()
        /// </summary>
        /// <param name="channel">Channel where the mention happened</param>
        /// <param name="link">The mention</param>
        /// <param name="offset">Which word it'll be on</param>
        /// <returns>DiscordMember that got mentioned</returns>
        public DiscordMember GetMemberFromLink(DiscordChannel channel, string link, int offset = 1)
        {
            string output = link;
            output = link.Split(' ')[offset];
            output = output.Trim(new char[] { '@', '<', '>' });
            return Discord.client.GetMemberFromChannel(channel, output);
        }
    }
}
