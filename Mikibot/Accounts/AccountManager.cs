using DiscordSharp.Objects;
using Miki.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

namespace Miki.Accounts
{
    public class AccountManager
    {
        private List<Account> accounts = new List<Account>();

        public void AddAccount(Account a)
        {
            if (!isLoggedIn(a.GetMember()))
            {
                Console.WriteLine("Added member " + a.GetMember().Username);
                accounts.Add(a);
            }
        }

        public void AddAccount(DiscordMember a, DiscordChannel c)
         {
            if (!isLoggedIn(a))
            {
                CreateAccountFromMember(a, c);
            }
        }

        public void RemoveAccount(Account a)
        {
            if (isLoggedIn(a.GetMember()))
            {
                Console.WriteLine("Removed member " + a.GetMember().Username);
                accounts.Remove(a);
            }
        }

        public void RemoveAccount(DiscordMember a)
        {
            if (isLoggedIn(a))
            {
                Console.WriteLine("Removed member " + a.Username);
                accounts.Remove(GetAccountFromMember(a));
            }
        }

        public void LoadAllAccounts()
        {
            if (!Directory.Exists(Global.AccountsFolder))
            {
                Directory.CreateDirectory(Global.AccountsFolder);
            }
            string[] allCP = Directory.GetDirectories(Global.AccountsFolder);
            for (int i = 0; i < allCP.Length; i++)
            {
                string input = allCP[i].Split('.')[0];
                input = input.Split('/')[input.Split('/').Length - 1];
                if(!isLoggedIn(input))
                {

                }
            }
            Log.Done("total pasta's loaded: " + allCP.Length);
        }

        public void SaveAllAccounts()
        {
            lock (accounts)
            {
                for (int i = 0; i < accounts.Count; i++)
                {
                    accounts[i].SaveProfile();
                }
            }
            Console.WriteLine("Saved all accounts!");
            Thread.Sleep(300000);
            SaveAllAccounts();
        }

        public void SaveAccountsOnCommand()
        {
            lock (accounts)
            {
                for (int i = 0; i < accounts.Count; i++)
                {
                    accounts[i].SaveProfile();
                }
            }
        }

        public Account GetAccountFromMember(DiscordMember member)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (member == accounts[i].GetMember())
                {
                    return accounts[i];
                }
            }
            return null;
        }

        public Account CreateAccountFromMember(DiscordMember member, DiscordChannel channel)
        {
            Account a = new Account();
            a.Login(member, channel);
            return a;
        }

        public Account GetAccountFromID(string ID)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (ID == accounts[i].GetMember().ID)
                {
                    return accounts[i];
                }
            }
            return null;
        }

        public Account[] GetAccountLeaderboards(bool local, string ID)
        {
            Account[] output = new Account[10];
            List<Account> tempaccounts;
            tempaccounts = accounts;
            tempaccounts.Sort((a, b) => { return b.profile.Experience.CompareTo(a.profile.Experience); });
            for (int i = 0; i < tempaccounts.Count; i++)
            {
                if (tempaccounts[i].GetMember().IsBot)
                {
                    tempaccounts.Remove(tempaccounts[i]);
                }
                if(local)
                {

                }
            }
            for (int i = 0; i < ((tempaccounts.Count > 10) ? 10 : tempaccounts.Count); i++)
            {
                output[i] = tempaccounts[i];
            }
            return output;
        }

        public string GetProfile(string member)
        {
            Account a = GetAccountFromID(member);
            if (a != null)
            {
                string output = "";
                output += "**" + a.GetMember().Username + "**\n";
                output += "**Level**: " + a.profile.Level + " (exp " + a.profile.Experience + "/" + a.profile.MaxExperience + ")\n\n";
                output += "__**Achievement**__\n";
                output += a.achievements.PrintAchieved();
                return output;
            }
            return "";
        }

        public bool isLoggedIn(DiscordMember m)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].GetMember().ID == m.ID)
                {
                    return true;
                }
            }
            return false;
        }

        public bool isLoggedIn(string ID)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (accounts[i].GetMember().ID == ID)
                {
                    return true;
                }
            }
            return false;
        }
    }
}