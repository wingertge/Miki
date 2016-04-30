using DiscordSharp.Events;
using Miki.Core;
using Miki.Core.Command;
using Newtonsoft.Json;
using System.Net;

namespace Miki.Extensions.RandomCatExtension
{
    internal class RandomCat : Command
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