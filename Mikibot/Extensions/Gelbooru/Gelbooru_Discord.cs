using Miki.Core;
using System;
using System.Xml;
using System.Xml.Linq;
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

namespace Miki.Extensions.Gelbooru
{
    //082f05ea7d67b14   -   33354c5a255e911f65bf63c3d9b92b9dfc6ac896
    class GelBooru_Discord : Command
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
            string tag = command[1];
            string nsfwTag = (command[command.Length - 1] == "-nsfw") ? " rating:explicit" : " rating:safe";
            if (tag == "awoo")
            {
                tag = "inubashiri_momiji";
            }
            XmlDocument myDoc = new XmlDocument();
            myDoc.Load("http://gelbooru.com/index.php?page=dapi&s=post&q=index&limit=1&tags=" + tag + nsfwTag);

            string details = myDoc["Posts"]["Post"]["file_url"].InnerText;

            /*int randNumber = r.Next(0, d.Count);
            if (d.Count > 0)
            {
                e.Channel.SendMessage("http://gelbooru.com" + d[randNumber].file_url);
            }
            */

            e.Channel.SendMessage(details);
            base.PlayCommand(e);
        }
    }
}