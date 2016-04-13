using System;
using System.IO;

namespace Miki
{
    public class Profile
    {
        string name;

        public int Health;
        public int Experience;
        public int MaxExperience;
        public int Level;
        public int Wins;

        public void Initialize(string name)
        {
            this.name = name;
            Level = 1;
            Experience = 0;
            MaxExperience = 60;
            Health = 20;
            Wins = 0;
        }

        public void AddHealth(int health)
        {
            Health += health;
        }
        public void SetHealth(int health)
        {
            Health = health;
        }
        public void AddExp(int exp)
        {
            Experience += exp;
            if (CanLevelUp())
            {
                Level++;
                MaxExperience = (int)Math.Round(MaxExperience * 2.5);
                Discord.channel.SendMessage(name + " has levelled up! (" + (Level - 1) + " -> " + Level + ")\n");
            }
        }

        public bool CanLevelUp()
        {
            return (Experience >= MaxExperience);
        }

        public void SaveProfile(string id)
        {
            if (!Directory.Exists(GlobalVariables.AccountsFolder + id))
            {
                Directory.CreateDirectory(GlobalVariables.AccountsFolder + id);
            }
            StreamWriter sw = new StreamWriter(GlobalVariables.AccountsFolder + id + ".profile");
            sw.WriteLine(Health.ToString());
            sw.WriteLine(Experience);
            sw.WriteLine(MaxExperience);
            sw.WriteLine(Level);
            sw.WriteLine(Wins);
            sw.Close();
        }
        public void LoadProfile(string id)
        {
            if (!Directory.Exists(GlobalVariables.AccountsFolder + id))
            {
                return;
            }
            StreamReader sr = new StreamReader(GlobalVariables.AccountsFolder + id + ".profile");
            Health = int.Parse(sr.ReadLine());
            Experience = int.Parse(sr.ReadLine());
            MaxExperience = int.Parse(sr.ReadLine());
            Level = int.Parse(sr.ReadLine());
            Wins = int.Parse(sr.ReadLine());
            sr.Close();
        }
    }
}