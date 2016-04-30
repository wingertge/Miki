using DiscordSharp.Objects;
using Miki.Accounts.Profiles;
using Miki.Core;
using System;
using System.IO;

namespace Miki.Accounts
{
    public class Account
    {
        public bool isDeveloper = false;

        public DateTime timeOfCreation;
        public string memberID;
        public Profile profile;
        public AchievementsManager achievements;
        public WordsSpoken wordsSpoken;
        public string lastActiveChannel;

        private DateTime lastExpTime;

        private void Initialize()
        {
            profile = new Profile();
            profile.Initialize(GetMember(memberID).Username, this);
            wordsSpoken = new WordsSpoken();
            achievements = new AchievementsManager();
            achievements.Initialize(this);
            wordsSpoken.Initialize();
        }

        public void Create(DiscordMember member, DiscordChannel channel)
        {
            this.memberID = member.ID;
            lastActiveChannel = channel.ID;
            Initialize();
            timeOfCreation = DateTime.Now;
            SaveProfile();
            Discord.account.AddAccount(this);
        }

        public void Login(DiscordMember member, DiscordChannel channel)
        {
            memberID = member.ID;
            lastActiveChannel = channel.ID;
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

        public DiscordMember GetMember()
        {
            return Discord.client.GetMemberFromChannel(GetChannel(), memberID);
        }

        public DiscordMember GetMember(string ID)
        {
            return Discord.client.GetMemberFromChannel(GetChannel(), ID);
        }

        public DiscordChannel GetChannel()
        {
            return Discord.client.GetChannelByID(long.Parse(lastActiveChannel));
        }

        private bool canGetXP()
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
            SetChannel(c);
        }

        public void SetChannel(DiscordChannel c)
        {
            lastActiveChannel = c.ID;
        }

        public void SaveProfile()
        {
            if (!Directory.Exists(Global.AccountsFolder + memberID))
            {
                Directory.CreateDirectory(Global.AccountsFolder + memberID);
            }
            StreamWriter sw = new StreamWriter(Global.AccountsFolder + memberID + "/" + memberID + ".sav");
            sw.WriteLine(timeOfCreation.ToString());
            sw.WriteLine(isDeveloper.ToString());
            sw.Close();
            profile.SaveProfile(memberID);
            achievements.SaveProfile(memberID);
        }

        public void LoadProfile()
        {
            if (!Directory.Exists(Global.AccountsFolder + memberID))
            {
                Create(GetMember(memberID), GetChannel());
                return;
            }
            StreamReader sr = new StreamReader(Global.AccountsFolder + memberID + "/" + memberID + ".sav");
            timeOfCreation = DateTime.Parse(sr.ReadLine());
            sr.Close();
            profile.LoadProfile(memberID);
            achievements.LoadProfile(memberID);
        }
    }
}