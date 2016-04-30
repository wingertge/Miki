using DiscordSharp.Events;
using Miki.Extensions.POSTLinux;
using Newtonsoft.Json;

namespace Miki.Extensions.Cleverbot
{
    public class Cleverbot
    {
        private POST p = new POST();

        private string nick = "";

        private string user = "CAPLr2gXFrzj1IwC";
        private string key = "d9PEW2RqmumO2wH68Vn7gZM1PJ3vDKjx";

        public void GetAsked(DiscordMessageEventArgs e)
        {
            string text = e.MessageText.Split('>')[1].TrimStart(' ');
            string result = p.UploadString(new CleverbotAskCredentials(user, key, nick, text), "https://cleverbot.io/1.0/ask");
            CleverbotOnAskEvent responseAsk = JsonConvert.DeserializeObject<CleverbotOnAskEvent>(result);
            if (responseAsk.status == "success")
            {
                e.Channel.SendMessage(responseAsk.response);
            }
        }
    }
}