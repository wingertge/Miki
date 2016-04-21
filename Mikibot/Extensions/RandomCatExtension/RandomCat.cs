using Miki.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;
using System.Net;
using Miki.Core.Debug;
using Newtonsoft.Json;

namespace Miki.Extensions.RandomCatExtension
{
    class RandomCat:Command
    {
        public override void Initialize()
        {
            id = "cat";
            appearInHelp = true;
            description = "recieve a random cat";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            WebClient c = new WebClient();
            byte[] b = c.DownloadData("http://random.cat/meow");
            var str = System.Text.Encoding.Default.GetString(b);
            Cat cat = JsonConvert.DeserializeObject<Cat>(str);
            e.Channel.SendMessage(cat.file);
            base.PlayCommand(e);
        }
    }
}
