using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Miki.Core.Config
{
    public class ConfigManager
    {
        public Bitmap Avatar;

        public void Initialize()
        {
            if (Debugger.IsAttached)
            {
                Discord.client = new DiscordSharp.DiscordClient("MTY5OTAwMjc4MTMxMzI2OTc2.CfE-UA.Mo8XihTgIEH50ryAcIqZzDvtCKA", true, true);
            }
            
        }

        public void LoadConfig()
        {
            StreamReader r = new StreamReader(Global.ConfigFile);
            Global.AvatarImage = r.ReadLine().Split('=')[1];
        }
    }
}
