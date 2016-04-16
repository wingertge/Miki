using Miki.Core.Debug;
using System.Diagnostics;

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
                    if (Debugger.IsAttached)
                    {
                        PlayCommand(e);
                    }
                    else
                    {
                        try
                        {
                            PlayCommand(e);
                        }
                        catch
                        {
                            Log.Error("command: " + id);
                        }
                    }
                }
            }
            else
            {
                if (message == id)
                {
                    if (Debugger.IsAttached)
                    {
                        PlayCommand(e);
                    }
                    else
                    {
                        try
                        {
                            PlayCommand(e);
                        }
                        catch
                        {
                            Log.Error("command: " + id);
                        }
                    }
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
