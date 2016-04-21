using DiscordSharp.Events;
using Miki.Core.Debug;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using JsonRequest;

namespace Miki.Extensions.Cleverbot
{
    public class Cleverbot
    {
        string nick = "";

        string user = "CAPLr2gXFrzj1IwC";
        string key = "d9PEW2RqmumO2wH68Vn7gZM1PJ3vDKjx";

        public void Initialize()
        {
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            var random = new Random();
            nick = new string(Enumerable.Repeat(chars, 16).Select(s => s[random.Next(s.Length)]).ToArray());

            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString("https://cleverbot.io/1.0/create", "POST", JsonConvert.SerializeObject(new CleverbotCreateCredentials(user, key, nick)));
            }
            CleverbotOnCreateEvent e = JsonConvert.DeserializeObject<CleverbotOnCreateEvent>(result);
            if(e.status == "success")
            {
                Log.Done(nick + " connected to cleverbot");
            }
        }

        public void GetAsked(DiscordMessageEventArgs e)
        {
            string text = e.MessageText.Split('>')[1].TrimStart(' ');

            string result = "";
            using (var client = new WebClient())
            {
                client.Headers[HttpRequestHeader.ContentType] = "application/json";
                result = client.UploadString("https://cleverbot.io/1.0/ask", "POST", JsonConvert.SerializeObject(new CleverbotAskCredentials(user, key, nick, text)));
            }
            CleverbotOnAskEvent response = JsonConvert.DeserializeObject<CleverbotOnAskEvent>(result);
            if (response.status == "success")
            {
                e.Channel.SendMessage(response.response);
            }

        }
    }
}
