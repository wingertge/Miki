namespace Miki.Core
{
    internal class ServerData
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