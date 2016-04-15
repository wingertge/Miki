using Miki.Core.Debug;

namespace Miki.Core
{
    public class Command
    {
        protected string id;
        protected bool isPublic;
        protected bool hasParameters;

        protected string usage;
        protected string description;

        protected string message;

        public virtual void Initialize() {
            Log.Message("Loaded Command: " + id);
        }

        public virtual void CheckCommand(DiscordSharp.Events.DiscordMessageEventArgs e)
        {
            message = e.MessageText.Trim(new char[] { '>' });
            message = message.ToLower();
            if (hasParameters)
            {
                if (message.StartsWith(id + " "))
                {
                    PlayCommand(e);
                }
            }
            else
            {
                if (message == id)
                {
                    PlayCommand(e);
                }
            }
        }

        protected virtual void PlayCommand(DiscordSharp.Events.DiscordMessageEventArgs e)
        {

        }
    
        public string GetHelpLine()
        {
            return ">" + id + ": " + description;
        }
    }
}
