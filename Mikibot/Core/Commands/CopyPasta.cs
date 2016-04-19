using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;
using System.IO;
using Miki.Core.Debug;

namespace Miki.Core.Commands
{
    class Copypasta : Command
    {
        public static string CopypastaFolder = Global.MikiFolder + "Commands/Copypasta/";
        Dictionary<string, string> Pasta = new Dictionary<string, string>();

        public override void Initialize()
        {
            id = "pasta";
            appearInHelp = true;
            description = "throw in your favourite copy pasta's";
            hasParameters = true;
            usage = new string[] { "tag or 'add'" };

            if(!Directory.Exists(CopypastaFolder))
            {
                Directory.CreateDirectory(CopypastaFolder);
            }
            string[] allCP = Directory.GetFiles(CopypastaFolder);
            for(int i = 0; i < allCP.Length; i++)
            {
                string input = allCP[i].Split('.')[0];
                input = input.Split('/')[input.Split('/').Length -1];
                LoadCopypasta(input);
            }

            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            string[] parameters = message.Split(' ');
            if (parameters[1] == "new")
            {
                AddCopypasta(parameters[2], message.Substring(parameters[2].Length + 11));
                e.Channel.SendMessage("Added copypasta '" + parameters[2] + "'!");
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
            Log.Message("Loaded copypasta: " + tag);
        }
    }
}
