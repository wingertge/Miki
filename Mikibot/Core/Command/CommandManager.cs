using Miki.Core.Command.Debug;
using Miki.Core.Command.Objects;
using Miki.Core.Debug;
using Miki.Extensions.osu;
using Miki.Extensions.RandomCatExtension;
using System.Collections.Generic;

namespace Miki.Core.Command
{
    public class CommandManager
    {
        public static List<Command> commands = new List<Command>();

        /// <summary>
        /// Loads all the command in the RAM
        /// </summary>
        public void LoadCommands()
        {
            commands.Add(new BlacklistServer());
            commands.Add(new RandomCat());
            commands.Add(new Changelog());
            commands.Add(new Extensions.SillyCompliments.SillyCompliments_Core());
            commands.Add(new Extensions.Danbooru.DanBooru_Discord());
            commands.Add(new Extensions.Safebooru.SafeBooru_Discord());
            commands.Add(new Extensions.Cage.Cage_Discord());
            commands.Add(new ErrorCount());
            commands.Add(new FizzbuzzCommand());
            commands.Add(new Extensions.Gelbooru.GelBooru_Discord());
            commands.Add(new HelpCommand());
            commands.Add(new Hug());
            commands.Add(new Smug());
            commands.Add(new Extensions.IMDb.IMDbNet_Discord());
            commands.Add(new InfoCommand());
            commands.Add(new Accounts.Commands.TopProfiles());
            commands.Add(new osu_core());
            commands.Add(new Copypasta());
            commands.Add(new Pat());
            commands.Add(new Ping());
            commands.Add(new Poke());
            commands.Add(new Accounts.Commands.Profile());
            commands.Add(new ChangeUsername());
            commands.Add(new RequestIdea());
            commands.Add(new Roll());
            commands.Add(new Roulette());
            commands.Add(new ForceSave());
            commands.Add(new Statistics());
            commands.Add(new UpdateAvatar());
            commands.Add(new Uptime());
            commands.Add(new Whoami());
            commands.Add(new Whoisserver());

            for (int i = 0; i < commands.Count; i++)
            {
                commands[i].Initialize();
            }
            Log.Done("Loaded " + commands.Count + " commands!");
        }
    }
}