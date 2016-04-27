using DiscordSharp.Objects;
using Miki.Core;
using Miki.Core.Debug;
using System;
using System.IO;

namespace Miki.Accounts.Profiles
{
    public class Profile
    {
        string name;

        public int Health;
        public int Experience;
        public int MaxExperience;
        public int Level;
        public int Wins;

        Account parent;

        public void Initialize(string name, Account parent)
        {
            this.name = name;
            Level = 1;
            Experience = 0;
            MaxExperience = 60;
            Health = 20;
            Wins = 0;

            this.parent = parent;
        }

        public void AddExp(int exp)
        {
            Experience += exp;
            if (CanLevelUp())
            {
                Level++;
                MaxExperience = (int)Math.Round(MaxExperience * 2.5);
                Console.WriteLine(name + " has levelled up! (" + (Level - 1) + " -> " + Level + ")");
                parent.GetChannel().SendMessage(name + " has levelled up! (" + (Level - 1) + " -> " + Level + ")\n");
            }
        }

        public bool CanLevelUp()
        {
            return (Experience >= MaxExperience);
        }

        public void SaveProfile(string id)
        {
            if (!Directory.Exists(Global.AccountsFolder + id + "/"))
            {
                Directory.CreateDirectory(Global.AccountsFolder + id + "/");
            }
            StreamWriter sw = new StreamWriter(Global.AccountsFolder + id + "/" + id + ".profile");
            sw.WriteLine(Health.ToString());
            sw.WriteLine(Experience);
            sw.WriteLine(MaxExperience);
            sw.WriteLine(Level);
            sw.WriteLine(Wins);
            sw.Close();
        }
        public void LoadProfile(string id)
        {
            if (!Directory.Exists(Global.AccountsFolder + id + "/"))
            {
                Log.Warning(id + " does not exist");
                return;
            }
            StreamReader sr = new StreamReader(Global.AccountsFolder + id + "/" + id + ".profile");
            Health = int.Parse(sr.ReadLine());
            Log.Message(Global.AccountsFolder + id + "/" + id + ".profile");
            Experience = int.Parse(sr.ReadLine());
            MaxExperience = int.Parse(sr.ReadLine());
            Level = int.Parse(sr.ReadLine());
            Wins = int.Parse(sr.ReadLine());
            sr.Close();
        }
    }
}