using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DiscordSharp.Objects;
using System.IO;
using Miki.Accounts.Profiles;
using Miki.Core;
using Miki.Core.Debug;

namespace Miki.Accounts
{
    public class Account
    {
        public bool isDeveloper = false;

        public DateTime timeOfCreation;
        public DiscordMember member;
        public Profile profile;
        public AchievementsManager achievements;
        public WordsSpoken wordsSpoken;

        DateTime lastExpTime;

        void Initialize()
        {
            profile = new Profile();
            profile.Initialize(member.Username);
            wordsSpoken = new WordsSpoken();
            wordsSpoken.Initialize();
        }
        public void Create(DiscordMember member)
        {
            Log.Message("Creating account: " + member.ID);
            this.member = member;
            Initialize();
            timeOfCreation = DateTime.Now;
            SaveProfile();
            Discord.account.AddAccount(this);
        }
        public void Login(DiscordMember member)
        {
            this.member = member;
            Initialize();
            LoadProfile();
            Discord.account.AddAccount(this);
        }

        public void AddExp(int exp)
        {
            profile.AddExp(exp);
        }
        public int GetLevel()
        {
            return profile.Level;
        }

        bool canGetXP()
        {
            return (lastExpTime.AddSeconds(15) <= DateTime.Now);
        }

        public void OnMessageRecieved(DiscordChannel c)
        {
            if (canGetXP())
            {
                profile.AddExp(1);
                lastExpTime = DateTime.Now;
            }
            wordsSpoken.MessagesSent++;
            profile.SetChannel(c);
        }

        public void SaveProfile()
        {
            if(!Directory.Exists(Global.AccountsFolder + member.ID))
            {
                Directory.CreateDirectory(Global.AccountsFolder + member.ID);
            }
            StreamWriter sw = new StreamWriter(Global.AccountsFolder + member.ID + "/" + member.ID + ".sav");
            sw.WriteLine(timeOfCreation.ToString());
            sw.WriteLine(isDeveloper.ToString());
            sw.Close();
            profile.SaveProfile(member.ID);
        }
        public void LoadProfile()
        {
            if (!Directory.Exists(Global.AccountsFolder + member.ID))
            {
                Create(member);
                return;
            }
            StreamReader sr = new StreamReader(Global.AccountsFolder + member.ID + "/" + member.ID + ".sav");
            timeOfCreation = DateTime.Parse(sr.ReadLine());
            //isDeveloper = bool.Parse(sr.ReadLine());
            sr.Close();
            profile.LoadProfile(member.ID);
        }
    }
}
