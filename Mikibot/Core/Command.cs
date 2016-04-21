using Miki.Core.Debug;
using System.Diagnostics;

namespace Miki.Core
{
    public enum ParameterType { YES, NO, BOTH };

    public class Command
    {
        protected string id;
        protected bool appearInHelp;
        protected ParameterType parameterType = ParameterType.NO;

        protected string[] usage;
        protected string description;

        protected string expandedDescription;

        protected string message;

        /// <summary>
        /// We add our variables here.
        /// (REQUIRED) id (the command that will be ran)
        /// (REQUIRED) appearInHelp
        /// (REQUIRED) description
        /// (Optional) hasParameters
        /// (Optional) usage
        /// </summary>
        public virtual void Initialize() {
        }

        /// <summary>
        /// This function checks if the command has been triggered by the Message Event.
        /// 
        /// You shouldn't have to call this.
        /// </summary>
        /// <param name="e">message recieved from discord</param>
        public virtual void CheckCommand(DiscordSharp.Events.DiscordMessageEventArgs e)
        {
            message = e.MessageText.Trim(new char[] { '>' });
            message = message.ToLower();
            if (parameterType == ParameterType.YES)
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
            else if(parameterType == ParameterType.NO)
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
            else
            {
                if (message.StartsWith(id))
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

        /// <summary>
        /// This function will be called whenever the player triggered the command based on it's id.
        /// 
        /// Call your command functionality here.
        /// </summary>
        /// <param name="e">message recieved from discord</param>
        protected virtual void PlayCommand(DiscordSharp.Events.DiscordMessageEventArgs e)
        {

        }

        /// <summary>
        /// We call this to get the >help command. all commands will return this line in a list.
        /// </summary>
        /// <returns>>dan: gets images from danbooru</returns>
        public string GetHelpLine()
        {
            if (appearInHelp)
            {
                return "`>" + id + "`: " + description + '\n';
            }
            return "";
        }

        public string GetAllUsageTags()
        {
            string output = usage[0];
            for(int i = 1; i < usage.Length; i++)
            {
                output += "," + usage[i];
            }
            return output;
        }
    }
}
