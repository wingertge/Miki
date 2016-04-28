using DiscordSharp.Events;
using Miki.Core;
using System;

namespace Miki.Core.Commands
{
    class Smug :Command
    {
        Random r = new Random();

        String[] smug = new String[]
        {
            "http://i.imgur.com/zUwqrhM.png",
            "http://i.imgur.com/TYqPh89.jpg",
            "http://i.imgur.com/xyOSaCt.png",
            "http://i.imgur.com/gyw0ifl.png",
            "http://i.imgur.com/kk0xvtx.png",
            "http://i.imgur.com/UIuyUne.jpg",
            "http://i.imgur.com/9zgIjY1.jpg",
            "http://i.imgur.com/Ku1ONAD.jpg",
            "http://i.imgur.com/7lB5bRT.jpg",
            "http://i.imgur.com/BoVHipF.jpg",
            "http://i.imgur.com/vN48mwz.png",
            "http://i.imgur.com/fGI4zLe.jpg",
            "http://i.imgur.com/Gc4gmwQ.jpg",
            "http://i.imgur.com/JMrmKt7.jpg",
            "http://i.imgur.com/a7sbJz2.jpg",
            "http://i.imgur.com/NebmjhR.png",
            "http://i.imgur.com/5ccbrFI.png",
            "http://i.imgur.com/XJL4Vmo.jpg",
            "http://i.imgur.com/eg0q1ez.png",
            "http://i.imgur.com/JJFxxmA.jpg",
            "http://i.imgur.com/2cTDF3b.jpg",
            "http://i.imgur.com/Xc0Duqv.png",
            "http://i.imgur.com/YgMdPkd.jpg",
            "http://i.imgur.com/BvAv6an.jpg",
            "http://i.imgur.com/KRLP5JT.jpg",
            "http://i.imgur.com/yXcsCK3.jpg",
            "http://i.imgur.com/QXG56kG.png",
            "http://i.imgur.com/OFBz1YJ.png",
            "http://i.imgur.com/9ulVckY.png",
            "http://i.imgur.com/VLXeSJK.png",
        };

        public override void Initialize()
        {
            id = "smug";
            appearInHelp = true;
            description = "Randomly picks a picture of a smug anime girl";

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            base.PlayCommand(e);
            e.Channel.SendMessage(smug[r.Next(0, smug.Length)]);
        }

    }
}
