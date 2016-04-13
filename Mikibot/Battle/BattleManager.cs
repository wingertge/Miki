using System;
using System.Collections.Generic;
using DiscordSharp.Objects;

namespace Miki
{
    public class BattleManager
    {
        Account invitedBattler;
        DateTime timeOfInvitation = DateTime.MinValue;
        public bool allowJoin;
        int Turn;

        List<Account> profiles = new List<Account>();

        public void StartBattle(Account[] battlers, bool allowjoin)
        {
            allowJoin = allowjoin;
            if (!allowjoin)
            {
                profiles = new List<Account>();
                profiles.Add(battlers[0]);
                if (!isWaitingForInvite())
                {
                    battlers[1].member.SendMessage("** You've been challanged for a DUEL**\n Type '$accept' **in PM** to Fight\n Type '$decline' or ignore this message to not.");
                    invitedBattler = battlers[1];
                    timeOfInvitation = DateTime.Now;
                }
            }
            else
            {
                for (int i = 0; i < battlers.Length; i++)
                {
                    profiles.Add(battlers[i]);
                }
                Discord.channel.SendMessage("**Battle set up, waiting for moves**\n" +
                battlers[1].member.Username + " has recieved a bonus of " + (5 + Math.Floor(0.5 * battlers[1].GetLevel())) + "HP for being defendant");
                battlers[1].AddHealth(5 + (int)Math.Floor(0.5 * battlers[1].GetLevel()));
                Discord.channel.SendMessage("@" + profiles[Turn].member.Username + " 's Turn!");
            }
        }
        public void AcceptBattle(Account account)
        {
            if (invitedBattler.member == account.member)
            {
                Discord.channel.SendMessage("**Battle set up, waiting for moves**\n" +
                account.member.Username + " has recieved a bonus of " + (5 + Math.Floor(0.5 * account.GetLevel())) + "HP for being defendant");
                account.AddHealth(5 + (int)Math.Floor(0.5 * account.GetLevel()));
                Discord.channel.SendMessage("@" + profiles[Turn].member.Username + " 's Turn!");
            }
        }
        public void RefuseBattle(Account account)
        {
            if (invitedBattler == account)
            {
                invitedBattler = null;
                profiles = new List<Account>();
            }
        }
        public void JoinBattle(Account account)
        {
            if (allowJoin)
            {
                profiles.Add(account);
                Discord.channel.SendMessage("**" + account.member.Username + " joined the battle.**");
            }
            else
            {
                Discord.channel.SendMessage("** Duel in progress, you cannot join.**");
            }
        }
        public void EndBattle()
        {
            string output = "";
            for (int i = 0; i < profiles.Count; i++)
            {
                if (!allowJoin)
                {
                    output += profiles[i].member.Username + " has earned " + (5 + profiles[i].profile.Health) + " exp.\n";
                    profiles[i].AddExp(5 + profiles[i].profile.Health);
                }
                profiles[i].SetHealth(15 + (5 * profiles[i].profile.Level));
                profiles[i].SaveProfile();
            }
            profiles = new List<Account>();
            Turn = 0;
        }

        //Actions
        public void Attack(Account attacker, Account target)
        {
            Random r = new Random();
            int damage = r.Next(0, 10 + attacker.GetLevel());
            if (getBattleID(attacker.member) != -1)
            {
                target.AddHealth(-damage);
                if (damage > 0)
                {
                    Discord.channel.SendMessage(target.member.Username + " got hit by " + attacker.member.Username + " for " + damage + " damage!");
                }
                else
                {
                    Discord.channel.SendMessage(attacker.member.Username + " missed.");
                }
                if (target.profile.Health < 0)
                {
                    Discord.channel.SendMessage(target.member.Username + " died.");
                    EndBattle();
                }
                else
                {
                    EndTurn();
                }
            }
            else
            {
                Discord.channel.SendMessage(attacker.member.Username + " swung his weapon into nothing");
                EndTurn();
            }
        }
        public void EndTurn()
        {
            Turn++;
            if (Turn == profiles.Count)
            {
                Turn = 0;
            }
            Discord.channel.SendMessage("@" + profiles[Turn].member.Username + " 's Turn!");
            profiles[Turn].member.SendMessage("Your turn!");
        }

        public bool canAttack(DiscordMember member)
        {
            return (getBattleID(member) != -1 && isMyTurn(member));
        }
        public bool isBattling()
        {
            return (profiles.Count > 1);
        }
        public bool isWaitingForInvite()
        {
            return (timeOfInvitation.AddMinutes(1) > DateTime.Now) && invitedBattler != null;
        }
        public bool isMyTurn(DiscordMember member)
        {
            return getBattleID(member) == Turn;
        }
        public int getBattleID(DiscordMember member)
        {
            for(int i = 0; i < profiles.Count; i++)
            {
                if(profiles[i].member == member)
                {
                    return i;
                }
            }
            return -1;
        }
    }
}