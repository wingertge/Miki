using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Objects;
using System.IO;

namespace Miki
{
    public class Account
    {
        public bool isDeveloper = false;

        public DateTime timeOfCreation;
        public DiscordMember member;
        public Profile profile;
        public WordsSpoken wordsSpoken;

        void Initialize()
        {
            profile = new Profile();
            profile.Initialize(member.Username);
            wordsSpoken = new WordsSpoken();
            wordsSpoken.Initialize();
        }
        public void Create(DiscordMember member)
        {
            this.member = member;
            Initialize();
            timeOfCreation = DateTime.Now;
            SaveProfile();
            Discord.instance.account.AddAccount(this);
        }
        public void Login(DiscordMember member)
        {
            this.member = member;
            Initialize();
            LoadProfile();
            Discord.instance.account.AddAccount(this);
        }
        public void Logout(DiscordMember member)
        {
            if (Discord.instance.account.isLoggedIn(member))
            {
                SaveProfile();
                Discord.instance.account.RemoveAccount(member);
            }
        }

        public void AddExp(int exp)
        {
            profile.AddExp(exp);
        }
        public void AddHealth(int health)
        {
            profile.AddHealth(health);
        }
        public void SetHealth(int health)
        {
            profile.SetHealth(health);
        }

        public int GetLevel()
        {
            return profile.Level;
        }

        public void SaveProfile()
        {
            if(!Directory.Exists(GlobalVariables.AccountsFolder + member.ID))
            {
                Directory.CreateDirectory(GlobalVariables.AccountsFolder + member.ID);
            }
            StreamWriter sw = new StreamWriter(GlobalVariables.AccountsFolder + member.ID + ".sav");
            sw.WriteLine(timeOfCreation.ToString());
            sw.WriteLine(isDeveloper.ToString());
            sw.Close();
            profile.SaveProfile(member.ID);
        }
        public void LoadProfile()
        {
            if (!Directory.Exists(GlobalVariables.AccountsFolder + member.ID))
            {
                profile = null;
                return;
            }
            StreamReader sr = new StreamReader(GlobalVariables.AccountsFolder + member.ID + ".sav");
            timeOfCreation = DateTime.Parse(sr.ReadLine());
            //isDeveloper = bool.Parse(sr.ReadLine());
            sr.Close();
            profile.LoadProfile(member.ID);
        }
    }
}
