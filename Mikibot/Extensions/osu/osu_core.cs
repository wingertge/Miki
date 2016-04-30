using Miki.Core;
using Miki.Extensions.osu.Objects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;
using System.Net;
using Newtonsoft.Json;
using Miki.Core.Command;

namespace Miki.Extensions.osu
{
    class osu_core : Command
    {
        public override void Initialize()
        {
            id = "osu";
            parameterType = ParameterType.YES;
            description = "get stats from osu! from username";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            WebClient c = new WebClient();

            byte[] b;
            string[] command = e.MessageText.Split(' ');

            b = c.DownloadData("https://osu.ppy.sh/api/get_user?&k=ff4c6a52e7158c0f103813e1acb5c56b2d692bdc&u=" + command[1]);
            if (b != null)
            {
                string result = Encoding.UTF8.GetString(b);
                List<osu_user> d = JsonConvert.DeserializeObject<List<osu_user>>(result);

                e.Channel.SendMessage(d[0].username + " :flag_" + d[0].country.ToLower() + ":" +
                    "\n`Performance :` " + double.Parse(d[0].pp_raw).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("en-US")) + " (**#" + int.Parse(d[0].pp_rank).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("en-US")) + "**)" +
                    "\n`Level       :` " + Math.Floor(double.Parse(d[0].level)) + " (" + Math.Round(Math.Abs(Math.Floor(double.Parse(d[0].level)) - double.Parse(d[0].level)) * 100, 1) + "% to **" + (Math.Floor(double.Parse(d[0].level)) + 1) + "**)" +
                    "\n`Playcount   :` " + int.Parse(d[0].playcount).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("en-US")) +
                    "\n`Accuracy    :` " + Math.Round(double.Parse(d[0].accuracy), 2) + "% (300's: " + int.Parse(d[0].count300).ToString("N0", System.Globalization.CultureInfo.GetCultureInfo("en-US")) +  ")");
                    
            }
            base.PlayCommand(e);
        }
    }
}
