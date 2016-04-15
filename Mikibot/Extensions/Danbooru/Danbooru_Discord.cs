using Miki.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;
using System.Net;
using Newtonsoft.Json;
using System.Drawing;
using System.IO;
using Miki.Core.Debug;

namespace Miki.Extensions.Danbooru
{
    //082f05ea7d67b14   -   33354c5a255e911f65bf63c3d9b92b9dfc6ac896
    class DanBooru_Discord : Command
    {
        public override void Initialize()
        {
            id = "dan";
            isPublic = true;
            hasParameters = true;
            description = "get danbooru images";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            WebClient c = new WebClient();
            byte[] b;
            string[] command = e.MessageText.Split(' ');
            string tags = "";
            for(int i = 1; i < command.Length - 1; i++)
            {
                tags += command[i] + " ";
            }
            tags += (command[command.Length - 1] == "-nsfw") ? "rating:e " : command[command.Length - 1] + " rating:s";
            if (e.MessageText.Split(' ')[1] == "awoo")
            {
                b = c.DownloadData("http://danbooru.donmai.us/posts.json?tags=" + "inubashiri_momiji");
            }
            else
            {
                b = c.DownloadData("http://danbooru.donmai.us/posts.json?tags=" + tags);
            }
            string result = Encoding.UTF8.GetString(b);
            List<DanbooruPost> d = JsonConvert.DeserializeObject<List<DanbooruPost>>(result);
            Random r = new Random();
            int randNumber = r.Next(0, d.Count);
            if (d.Count > 0)
            {
                e.Channel.SendMessage("http://danbooru.donmai.us" + d[randNumber].file_url);
                Log.Message(d[randNumber].tag_string);
            }
            Log.Message("tags in command:" + tags);
            Log.Message("Danbooru command ended");
            base.PlayCommand(e);
        }
    }
}
