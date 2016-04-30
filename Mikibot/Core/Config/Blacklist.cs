using System.Collections.Generic;

namespace Miki.Core.Config
{
    public class Blacklist
    {
        public List<string> blacklist = new List<string>();

        /// <summary>
        /// Checks if a channel is blacklisted.
        /// </summary>
        /// <param name="id">Channel ID</param>
        /// <returns>true/false whether it's blacklisted or not.</returns>
        public bool isBlacklisted(string id)
        {
            for (int i = 0; i < blacklist.Count; i++)
            {
                if (blacklist[i] == id)
                {
                    return true;
                }
            }
            return false;
        }
    }
}