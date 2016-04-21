using Miki.Core.Commands;
using Miki.Core.Debug;
using System.Diagnostics;
using System.Drawing;
using System.IO;

namespace Miki.Core.Config
{
    public class ConfigManager
    {
        public Bitmap Avatar;
        public DiscordSharp.Objects.DiscordUserInformation info;

        /// <summary>
        /// Creates alot of data on C:/miki, and runs all basic tasks
        /// </summary>
        public void Initialize()
        {
            info = new DiscordSharp.Objects.DiscordUserInformation();
            if (!File.Exists(Global.ConfigFile))
            {
                Log.Warning("'Miki/Config.cfg' not found. Creating Folders/Files");
                SaveConfig();
            }
            Log.Message("Loading Config files");
            LoadConfig();
            Log.Message("Loading Commands");
            LoadCommands();
            Log.Message("Loading Blacklist");
            LoadBlacklist();
            if (Debugger.IsAttached)
            {
                Discord.client = new DiscordSharp.DiscordClient("MTY5OTAwMjc4MTMxMzI2OTc2.CfE-UA.Mo8XihTgIEH50ryAcIqZzDvtCKA", true, true);
            }
            if (Directory.Exists(Global.AvatarsFolder))
            {
                if (File.Exists(Global.Avatar))
                {
                    Avatar = (Bitmap)Bitmap.FromFile(Global.Avatar);
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
            Discord.client.UpdateCurrentGame("'>help' | v" + Global.VersionText);
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
        /// Loads all the command in the RAM
        /// </summary>
        void LoadCommands()
        {
            ChannelMessage.commands.Add(new BlacklistServer());
            ChannelMessage.commands.Add(new Extensions.SillyCompliments.SillyCompliments_Core());
            ChannelMessage.commands.Add(new Extensions.Danbooru.DanBooru_Discord());
            ChannelMessage.commands.Add(new ErrorCount());
            ChannelMessage.commands.Add(new FizzbuzzCommand());
            ChannelMessage.commands.Add(new HelpCommand());
            ChannelMessage.commands.Add(new Extensions.IMDb.IMDbNet_Discord());
            ChannelMessage.commands.Add(new InfoCommand());
            ChannelMessage.commands.Add(new Accounts.Commands.TopProfiles());
            ChannelMessage.commands.Add(new Copypasta());
            ChannelMessage.commands.Add(new Accounts.Commands.Profile());
            ChannelMessage.commands.Add(new RequestIdea());
            ChannelMessage.commands.Add(new Roll());
            ChannelMessage.commands.Add(new ForceSave());
            ChannelMessage.commands.Add(new Statistics());
            ChannelMessage.commands.Add(new UpdateAvatar());
            ChannelMessage.commands.Add(new Uptime());
            ChannelMessage.commands.Add(new Whoami());
            ChannelMessage.commands.Add(new Whoisserver());

            for(int i = 0; i < ChannelMessage.commands.Count; i++)
            {
                ChannelMessage.commands[i].Initialize();
            }
            Log.Done("Loaded " + ChannelMessage.commands.Count + " commands!");
        }

        /// <summary>
        /// Saves the blacklist to a file.
        /// </summary>
        public void SaveBlacklist()
        {
            StreamWriter s = new StreamWriter(Global.MikiFolder + "blacklist.blk");
            for(int i = 0; i < Discord.blacklist.blacklist.Count; i++)
            {
                s.WriteLine(Discord.blacklist.blacklist[i]);
            }
            s.Close();
        }

        /// <summary>
        /// Loads the blacklist from a file.
        /// </summary>
        void LoadBlacklist()
        {
            if(!File.Exists(Global.MikiFolder + "blacklist.blk"))
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
            s.WriteLine("Status=" + Discord.client.GetCurrentGame);
            if(info.Username != "")
            {
                s.WriteLine("Username=" + info.Username);
            }
            else
            {
                s.WriteLine("Username=Miki");
            }
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
            Global.Status = r.ReadLine().Split('=')[1];
            info.Username = r.ReadLine().Split('=')[1];
            r.Close();
        }
    }
}
