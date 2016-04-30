using Miki.Core.Command;
using Miki.Core.Debug;
using Miki.Extensions.RandomCatExtension;
using System.Drawing;
using System.IO;

namespace Miki.Core.Config
{
    public class ConfigManager
    {
        public Bitmap Avatar;

        /// <summary>
        /// Creates alot of data on C:/miki, and runs all basic tasks
        /// </summary>
        public void Initialize()
        {
            if (!File.Exists(Global.ConfigFile))
            {
                Log.Warning("'Miki/Config.cfg' not found. Creating Folders/Files");
                SaveConfig();
            }
            Log.Message("Loading Config files");
            LoadConfig();
            Log.Message("Loading Blacklist");
            LoadBlacklist();
            if (Directory.Exists(Global.AvatarsFolder))
            {
                if (File.Exists(Global.Avatar))
                {
                    Avatar = (Bitmap)Image.FromFile(Global.Avatar);
                }
                else
                {
                    Log.Warning("target avatar file not found: " + Global.Avatar);
                }
            }
            else
            {
                Log.Warning("No avatars folder found, creating one...");
                Directory.CreateDirectory(Global.AvatarsFolder);
            }
            Log.Done("Loading config files");
        }

        /// <summary>
        /// Loads data AFTER the bot has been connected to discord.
        /// </summary>
        public void OnConnectInitialize()
        {
            Discord.client.UpdateCurrentGame("'>help' | v" + Global.VersionNumber);
        }

        /// <summary>
        /// Sets new avatar
        /// </summary>
        public void UpdateAvatar()
        {
            if (Avatar != null)
            {
                Discord.client.ChangeClientAvatar(Avatar);
            }
            else
            {
                Log.Warning("No avatar file found.");
            }
        }

        /// <summary>
        /// Saves the blacklist to a file.
        /// </summary>
        public void SaveBlacklist()
        {
            StreamWriter s = new StreamWriter(Global.MikiFolder + "blacklist.blk");
            for (int i = 0; i < Discord.blacklist.blacklist.Count; i++)
            {
                s.WriteLine(Discord.blacklist.blacklist[i]);
            }
            s.Close();
        }

        /// <summary>
        /// Loads the blacklist from a file.
        /// </summary>
        private void LoadBlacklist()
        {
            if (!File.Exists(Global.MikiFolder + "blacklist.blk"))
            {
                Log.Warning("No blacklist file found, generating blank one...");
                SaveBlacklist();
            }
            StreamReader r = new StreamReader(Global.MikiFolder + "blacklist.blk");
            while (true)
            {
                string id = r.ReadLine();
                if (id != null)
                {
                    Discord.blacklist.blacklist.Add(id);
                }
                else
                {
                    break;
                }
            }
            r.Close();
        }

        /// <summary>
        /// Saves the config data.
        /// </summary>
        public void SaveConfig()
        {
            Directory.CreateDirectory(Global.AccountsFolder);
            Directory.CreateDirectory(Global.AvatarsFolder);
            StreamWriter s = new StreamWriter(Global.ConfigFile);
            s.WriteLine("Accounts=" + Global.AccountsFolder);
            s.WriteLine("Avatar=" + Global.AvatarImage);
            s.WriteLine("Key=" + Global.ApiKey);
            s.WriteLine("Status=" + Discord.client.GetCurrentGame);
            s.Close();
        }

        /// <summary>
        /// Loads the config data
        /// </summary>
        public void LoadConfig()
        {
            StreamReader r = new StreamReader(Global.ConfigFile);
            Global.AccountsFolder = r.ReadLine().Split('=')[1];
            Global.AvatarImage = r.ReadLine().Split('=')[1];
            Global.ApiKey = r.ReadLine().Split('=')[1];
            Global.Status = r.ReadLine().Split('=')[1];
            r.Close();
        }
    }
}