using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Extensions.Cleverbot
{
    class CleverbotCreateCredentials
    {
        public string user;
        public string key;
        public string nick;

        public CleverbotCreateCredentials(string _user, string _key, string _nick)
        {
            user = _user;
            key = _key;
            nick = _nick;
        }
    }
}
