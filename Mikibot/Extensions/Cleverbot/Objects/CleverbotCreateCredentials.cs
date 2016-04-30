namespace Miki.Extensions.Cleverbot
{
    internal class CleverbotCreateCredentials
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