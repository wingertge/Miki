using DiscordSharp.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki
{
    public class DiscordMemberHandler
    {
        public string GetIDFromLink(string link, int offset = 1)
        {
            string output = link;
            output = link.Split(' ')[offset];
            output = output.Trim(new char[] { '@', '<', '>' });
            return output;
        }
        public DiscordMember GetMemberFromLink(string link, int offset = 1)
        {
            string output = link;
            output = link.Split(' ')[offset];
            output = output.Trim(new char[] { '@', '<', '>' });
            return Discord.client.GetMemberFromChannel(Discord.channel, output);
        }
    }
}
