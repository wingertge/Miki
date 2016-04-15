using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Core.Config
{
    public class Blacklist
    {
        public List<string> blacklist = new List<string>();

        public bool isBlacklisted(string id)
        {
            for(int i = 0; i < blacklist.Count; i++)
            {
                if(blacklist[i] == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}
