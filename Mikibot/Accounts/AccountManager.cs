using DiscordSharp.Objects;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Miki.Accounts
{
    public class AccountManager
    {
        List<Account> accounts = new List<Account>();

        public void AddAccount(Account a)
        {
            if (!isLoggedIn(a.member))
            {
                Console.WriteLine("Added member " + a.member.Username);
                accounts.Add(a);
            }
        }
        public void AddAccount(DiscordMember a)
        {
            if (!isLoggedIn(a))
            {
                CreateAccountFromMember(a);
            }
        }
        public void RemoveAccount(Account a)
        {
            if (isLoggedIn(a.member))
            {
                Console.WriteLine("Removed member " + a.member.Username);
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

        public void SaveAllAccounts()
        {
            lock(accounts)
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
            for(int i = 0; i < accounts.Count; i++)
            {
                if(member == accounts[i].member)
                {
                    return accounts[i];
                }
            }
            return null;
        }

        public Account CreateAccountFromMember(DiscordMember member)
        {
            Account a = new Account();
            a.Login(member);
            return a;
        }

        public Account GetAccountFromID(string ID)
        {
            for (int i = 0; i < accounts.Count; i++)
            {
                if (ID == accounts[i].member.ID)
                {
                    return accounts[i];
                }
            }
            return null;

        }

        public Account[] GetAccountLeaderboards()
        {
            Account[] output = new Account[10];
            accounts.Sort((a, b) => { return b.profile.Experience.CompareTo(a.profile.Experience); });
            for (int i = 0; i < ((accounts.Count > 10) ? 10 : accounts.Count); i++)
            {
                if(accounts[i].member.IsBot)
                {
                    accounts.Remove(accounts[i]);
                    i--;
                }
                output[i] = accounts[i];
            }
            return output;
        }

        public string GetProfile(DiscordMember member)
        {
            Account a = GetAccountFromMember(member);
            if (a != null)
            {
                string output = "";
                output += "**" + member.Username + "**\n";
                output += "**Level**: " + a.profile.Level + " (exp " + a.profile.Experience + "/" + a.profile.MaxExperience + ")\n";
                return output;
            }
            return "";
        }

        public bool isLoggedIn(DiscordMember a)
        {
            for(int i = 0; i < accounts.Count; i++)
            {
                if(accounts[i].member.ID == a.ID)
                {
                    return true;
                }
            }
            return false;   
        }

    }
}
