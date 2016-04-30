using DiscordSharp.Events;
using Miki.Core.Command;

namespace Miki.Core
{
    public class ChannelMessage
    {
        private DiscordMessageEventArgs e;

        public ChannelMessage(DiscordMessageEventArgs e)
        {
            this.e = e;
        }

        /// <summary>
        /// This class checks all the commands that are initialized
        /// </summary>
        public void RecieveMessage()
        {
            if (e.MessageText.StartsWith(">"))
            {
                if (!e.Author.IsBot && e.Author.ID != "174668640065421313")
                {
                    for (int i = 0; i < CommandManager.commands.Count; i++)
                    {
                        CommandManager.commands[i].CheckCommand(e);
                    }
                }
                Discord.account.GetAccountFromID(e.Author.ID).achievements.CheckAllAchievements();
            }
        }
    }
}