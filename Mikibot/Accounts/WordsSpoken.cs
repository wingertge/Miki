using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Accounts
{
    public class WordsSpoken
    {
        string[] FilterWords;
        public int MessagesSent;

        public void Initialize()
        {
            FilterWords = GetWordsFromFile();
        }

        string[] GetWordsFromFile()
        {
            return null;
        }
    }
}
