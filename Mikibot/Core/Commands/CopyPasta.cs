using DiscordSharp.Events;
using Miki.Core.Debug;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Miki.Core.Command.Objects
{
    internal class Copypasta : Command
    {
        public static string CopypastaFolder = Global.MikiFolder + "Commands/Copypasta/";
        private Dictionary<string, string> Pasta = new Dictionary<string, string>();

        public override void Initialize()
        {
            Pasta = new Dictionary<string, string>();
            id = "pasta";
            appearInHelp = true;
            description = "throw in your favourite copypastas";
            parameterType = ParameterType.YES;
            usage = new string[] { "tag or 'add'" };

            if (!Directory.Exists(CopypastaFolder))
            {
                Directory.CreateDirectory(CopypastaFolder);
            }
            string[] allCP = Directory.GetFiles(CopypastaFolder);
            for (int i = 0; i < allCP.Length; i++)
            {
                string input = allCP[i].Split('.')[0];
                input = input.Split('/')[input.Split('/').Length - 1];
                LoadCopypasta(input);
            }
            Log.Done("total pasta's loaded: " + allCP.Length);
            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            string[] parameters = message.Split(' ');
            if (parameters[1].ToLower() == "new")
            {
                if (parameters.Length > 2)
                {
                    AddCopypasta(parameters[2].ToLower(), message.Substring(parameters[2].Length + 11));
                    e.Channel.SendMessage("Added copypasta '" + parameters[2] + "'!");
                    return;
                }
            }
            else if (parameters[1].ToLower() == "list")
            {
                string output = "";
                string[] list = Directory.GetFiles(CopypastaFolder);
                for (int i = 0; i < list.Length; i++)
                {
                    string path = "`";
                    path += list[i];
                    path = path.Split('/')[4];
                    path.Remove(path.Length - 3);
                    output += path + "` ";
                }
                e.Channel.SendMessage(output + "`");
                return;
            }
            else
            {
                try
                {
                    e.Channel.SendMessage(Pasta[parameters[1]]);
                }
                catch
                {
                    Log.Warning("Copypasta not found");
                    Thread.CurrentThread.Abort();
                }
            }
            base.PlayCommand(e);
        }

        public void AddCopypasta(string tag, string content)
        {
            if (!Directory.Exists(CopypastaFolder))
            {
                Directory.CreateDirectory(CopypastaFolder);
            }
            StreamWriter sw = new StreamWriter(CopypastaFolder + tag + ".cp");
            sw.WriteLine(content);
            Pasta.Add(tag, content);
            sw.Close();
            Log.Message("Added copypasta: " + tag);
        }

        public void LoadCopypasta(string tag)
        {
            if (!Directory.Exists(CopypastaFolder))
            {
                Directory.CreateDirectory(CopypastaFolder);
            }
            StreamReader sr = new StreamReader(CopypastaFolder + tag + ".cp");
            Pasta.Add(tag, sr.ReadLine());
            sr.Close();
        }
    }
}