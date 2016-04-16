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
            appearInHelp = true;
            hasParameters = true;
            description = "get danbooru images";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            Random r = new Random();
            WebClient c = new WebClient();

            byte[] b;
            string[] command = e.MessageText.Split(' ');
            string tag = command[1];
            string nsfwTag = (command[command.Length - 1] == "-nsfw") ? " rating:e" : " rating:s";
            if (tag == "awoo")
            {
                tag = "inubashiri_momiji";
            }
            b = c.DownloadData("http://danbooru.donmai.us/posts.json?tags=" + tag + nsfwTag);
            string result = Encoding.UTF8.GetString(b);
            List<DanbooruPost> d = JsonConvert.DeserializeObject<List<DanbooruPost>>(result);
            int randNumber = r.Next(0, d.Count);
            if (d.Count > 0)
            {
                e.Channel.SendMessage("http://danbooru.donmai.us" + d[randNumber].file_url);
            }
            base.PlayCommand(e);
        }
    }
}
