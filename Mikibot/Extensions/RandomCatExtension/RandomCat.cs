using Miki.Core;
using DiscordSharp.Events;
using System.Net;
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
            CatImage cat = JsonConvert.DeserializeObject<CatImage>(str);
            e.Channel.SendMessage(cat.file);
            base.PlayCommand(e);
        }
    }
}
