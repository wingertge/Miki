using Miki.Core.Debug;
using System.Diagnostics;

namespace Miki.Core.Command
{
    public enum ParameterType { YES, NO, BOTH };

    public class Command
    {
        protected string id;
        protected bool appearInHelp;
        protected bool devOnly;
        protected ParameterType parameterType = ParameterType.NO;

        protected string[] usage;
        protected string description;

        protected string expandedDescription;

        protected string message;

        public Command()
        {
        }

        public Command(string id, bool appearInHelp, ParameterType parameterType)
        {
            this.id = id;
            this.appearInHelp = appearInHelp;
            this.parameterType = parameterType;
        }

        public Command(string id, bool appearInHelp, bool devOnly, ParameterType parameterType)
        {
            this.id = id;
            this.appearInHelp = appearInHelp;
            this.devOnly = devOnly;
            this.parameterType = parameterType;
        }

        /// <summary>
        /// We add our variables here.
        /// (REQUIRED) id (the command that will be ran)
        /// (REQUIRED) appearInHelp
        /// (REQUIRED) description
        /// (Optional) usage
        /// (Optional) expandedDescription
        /// (Optional) devOnly
        /// </summary>
        public virtual void Initialize()
        {
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
            if (devOnly)
            {
                if (e.Author.ID != "121919449996460033")
                {
                    return;
                }
            }

            switch (parameterType)
            {
                case ParameterType.YES:
                    if (message.ToLower().StartsWith(id + " "))
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
                    break;

                case ParameterType.NO:
                    if (message.ToLower() == id)
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
                    break;

                case ParameterType.BOTH:
                    if (message.ToLower().StartsWith(id))
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
                    break;
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
            Log.Message("Command Triggered: " + id);
        }

        /// <summary>
        /// We call this to get the >help command. all commands will return this line in a list.
        /// </summary>
        /// <returns>>dan: gets images from danbooru</returns>
        public string GetHelpLine()
        {
            if (appearInHelp)
            {
                return "`>" + id + " " + GetSpace(12 - id.Length) + ":` __" + description + "__\n";
            }
            return "";
        }

        public string GetSpace(int amount)
        {
            string output = "";
            for (int i = 0; i < amount; i++)
            {
                output += " ";
            }
            return output;
        }

        public string GetAllUsageTags()
        {
            string output = usage[0];
            for (int i = 1; i < usage.Length; i++)
            {
                output += "," + usage[i];
            }
            return output;
        }
    }
}