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
            if (isLoggedIn(a.memberID) == -1)
            {
                accounts.Add(a);
            }
        }
        public void AddAccount(DiscordMember a, DiscordChannel channel)
        {
            if (isLoggedIn(a.ID) == -1)
            {
                CreateAccountFromMember(a, channel);
            }
        }
        public void RemoveAccount(Account a)
        {
            if (isLoggedIn(a.memberID) != -1)
            {
                accounts.Remove(a);
            }
        }
        public void RemoveAccount(DiscordMember a)
        {
            if (isLoggedIn(a.ID) != -1)
            {
                Console.WriteLine("Removed member " + a.Username);
                accounts.Remove(GetAccountFromMember(a.ID));
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

        public Account GetAccountFromMember(string ID)
        {
            for(int i = 0; i < accounts.Count; i++)
            {
                if(ID == accounts[i].memberID)
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
            if(isLoggedIn(ID) != -1)
            {
                return accounts[isLoggedIn(ID)];
            }
            return null;
        }

        public Account[] GetAccountLeaderboards()
        {
            Account[] output = new Account[10];
            accounts.Sort((a, b) => { return b.profile.Experience.CompareTo(a.profile.Experience); });
            for (int i = 0; i < ((accounts.Count > 10) ? 10 : accounts.Count); i++)
            {
                if(accounts[i].GetMember(accounts[i].memberID).IsBot)
                {
                    accounts.Remove(accounts[i]);
                    i--;
                }
                output[i] = accounts[i];
            }
            return output;
        }

        public string GetProfile(string ID)
        {
            Account a = GetAccountFromMember(ID);
            if (a != null)
            {
                string output = "";
                output += "**" + a.GetMember(a.memberID).Username + "**\n";
                output += "**Level**: " + a.profile.Level + " (exp " + a.profile.Experience + "/" + a.profile.MaxExperience + ")\n";
                return output;
            }
            return "";
        }

        public int isLoggedIn(string ID)
        {
            for(int i = 0; i < accounts.Count; i++)
            {
                if(accounts[i].memberID == ID)
                {
                    return i;
                }
            }
            return -1;   
        }

    }
}
