using DiscordSharp.Events;
using Miki.Core;
using Miki.Core.Command;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Text;

namespace Miki.Extensions.Gelbooru
{
    //082f05ea7d67b14   -   33354c5a255e911f65bf63c3d9b92b9dfc6ac896
    internal class GelBooru_Discord : Command
    {
        public override void Initialize()
        {
            id = "gel";
            appearInHelp = true;
            parameterType = ParameterType.YES;
            description = "get gelbooru images";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            Random r = new Random();
            WebClient c = new WebClient();

            byte[] b;
            string[] command = e.MessageText.Split(' ');

            List<string> tags = new List<string>();
            for (int i = 1; i < command.Length; i++)
            {
                if (command[i] == "awoo")
                {
                    tags.Add("inubashiri_momiji");
                    continue;
                }
                if (command[i] == "miki")
                {
                    tags.Add("sf-a2_miki");
                    continue;
                }
                if (command[i] == "-nsfw")
                {
                    tags.Add("rating:explicit");
                    continue;
                }
                tags.Add(command[i]);
            }
            if (!tags.Contains("rating:explicit"))
            {
                tags.Add("rating:safe");
            }

            b = c.DownloadData("http://gelbooru.com/index.php?page=dapi&s=post&q=index&json=1&tags=" + getTags(tags));
            if (b != null)
            {
                string result = Encoding.UTF8.GetString(b);
                List<GelbooruPost> d = JsonConvert.DeserializeObject<List<GelbooruPost>>(result);
                int randNumber = r.Next(0, d.Count);

                e.Channel.SendMessage(d[randNumber].file_url);
            }
            base.PlayCommand(e);
        }

        private string getTags(List<string> tags)
        {
            string output = "";
            for (int i = 0; i < tags.Count; i++)
            {
                output += tags[i] + " ";
            }
            return output;
        }
    }
}