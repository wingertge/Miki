using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Events;
using System.Threading;
using Miki.Core.Debug;

namespace Miki.Core.Commands
{
    class Changelog:Command
    {
        public static string ChangelogFolder = Global.MikiFolder + "Commands/Changelog/";

        Dictionary<string, string> entries;


        public override void Initialize()
        {
            entries = new Dictionary<string, string>();
            id = "changelog";
            appearInHelp = true;
            description = "check what's new in the latest update!";
            parameterType = ParameterType.BOTH;
            usage = new string[] { "version number" };

            if (!Directory.Exists(ChangelogFolder))
            {
                Directory.CreateDirectory(ChangelogFolder);
            }
            string[] allEntries = Directory.GetFiles(ChangelogFolder);
            Log.Message(allEntries.Length.ToString());
            for (int i = 0; i < allEntries.Length; i++)
            {
                Log.Message(allEntries[i]);
                string input = allEntries[i];
                input= input.Split('/')[input.Split('/').Length - 1];
                LoadEntry(input);
            }
            base.Initialize();
        }

        protected override void PlayCommand(DiscordMessageEventArgs e)
        {
            string[] parameters = message.Split(' ');
            if(parameters.Length == 1)
            {
                parameters = new string[] { parameters[0], Global.VersionNumber };
            }
            try
            {
                e.Channel.SendMessage(entries[parameters[1]]);
            }
            catch
            {
                Log.Warning("changelog not found");
                Thread.CurrentThread.Abort();
            }
            base.PlayCommand(e);
        }

        public void LoadEntry(string tag)
        {
            if (!Directory.Exists(ChangelogFolder))
            {
                Directory.CreateDirectory(ChangelogFolder);
            }
            StreamReader sr = new StreamReader(ChangelogFolder + tag);
            string input = "";
            while(true)
            {
                string newline = sr.ReadLine();
                input += newline + '\n';
                if(newline == null)
                {
                    break;
                }      
            }
            entries.Add(tag, input);
            sr.Close();
        }
    }
}
