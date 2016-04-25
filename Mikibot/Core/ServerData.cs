using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Core
{
    class ServerData
    {
        public ServerData(string k, string c)
        {
            key = k;
            servercount = c;
        }

        public string key;
        public string servercount;
    }
}
