using DiscordSharp;
using DiscordSharp.Objects;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace MikiBot
{
    class Discord
    {
        public static DiscordClient client = new DiscordClient("MTYwMTA1OTk0MjE3NTg2Njg5.Cczgog.61EAF2kEXTtq8DSiPg-ZT6SKFEo", true);
        public static DiscordChannel channel;
        public static Discord instance;

        public AccountManager account = new AccountManager();
        BattleManager battle = new BattleManager();

        /* Start of program, gets called from Program.Main() */
        public void Start()
        {
            Console.WriteLine("Starting Mikibot v" + Program.VersionNumber);
            instance = this;
            client.Connected += (sender, e) => { OnConnect(e); };
            client.UserAddedToServer += (sender, e) => { OnUserAdded(e); };
            client.PrivateMessageReceived += (sender, e) => { OnPrivateMessage(e); };
            client.MessageReceived += (sender, e) => { OnMessage(e); };
            client.SendLoginRequest();
            client.Connect();
            Console.ReadLine();
        }

        /* Event Listener: Gets called when Miki connects to the server */
        public void OnConnect(DiscordConnectEventArgs e)
        {
            Console.WriteLine("Connected! User: " + e.user.Username);
            client.UpdateCurrentGame("Mikibot v" + Program.VersionNumber);
            channel = client.GetChannelByID(160067691783127041);
        }

        /* Event Listener: Gets called whenever a DiscordMember gets added in the server. */
        public void OnUserAdded(DiscordSharp.Events.DiscordGuildMemberAddEventArgs e)
        {
            e.AddedMember.SendMessage("Welcome to Miki " + Program.VersionNumber +"\nTry '$login' to start!." );
        }

        /* Event Listener: Gets called whenever a PRIVATE message gets recieved by Miki */
        public void OnPrivateMessage(DiscordPrivateMessageEventArgs e)
        {
            // TOO TIRED TO FIX THIS. DO $DUEL ACCEPT IN ALL CHAT

            /* 
            Console.WriteLine(e.author.Username + " used duel");
            string response = e.message;
            if (response.Contains("accept"))
            {
                Console.WriteLine(e.author.Username + " accepted it");
                if (battle.isWaitingForInvite())
                {
                    Console.WriteLine(e.author.Username + " stuff after accept");
                    battle.AcceptBattle(account.GetAccountFromMember(e.author));
                }
            }
            else if (response.Contains("refuse"))
            {
                Console.WriteLine(e.author.Username + " refused it");
                if (battle.isWaitingForInvite())
                {
                    battle.RefuseBattle(account.GetAccountFromMember(e.author));
                }
            }*/
        }

        /* Event Listener: Gets called whenever a message gets recieved by Miki */
        public void OnMessage(DiscordSharp.Events.DiscordMessageEventArgs e)
        {
            string message = e.message_text.Trim(new char[] { '$' });
            message = message.ToLower();

            if (e.message_text.StartsWith("$"))
            {
                #region Standard bot information
                if (message == "help")
                {
                    e.Channel.SendMessage("wiki: https://github.com/velddev/Mikibot/wiki");
                    return;
                }
                if (message == "commands")
                {
                    e.Channel.SendMessage("commands: https://github.com/velddev/Mikibot/wiki/commands");
                    return;
                }
                if(message.StartsWith("fizzbuzz "))
                {
                    int input = 0;
                    try
                    {
                        input = int.Parse(message.Split(' ')[1]);
                        if(input == 0)
                        {
                            e.Channel.SendMessage("Nice try, kid");
                            return;
                        }
                    }
                    catch
                    {
                        e.Channel.SendMessage("That's not a number");
                        return;
                    }
                    string output = "";
                    if(input % 3 == 0)
                    {
                        output += "fizz";
                    }
                    if(input % 5 == 0)
                    {
                        output += "buzz";
                    }
                    if(output == "")
                    {
                        e.Channel.SendMessage(input.ToString());
                    }
                    else
                    {
                        e.Channel.SendMessage(output);
                    }
                    return;
                }
                #endregion
                #region moderation tools
                if (message == "bot-shutdown")
                {
                    if (e.author.Roles.Contains(client.GetRoles(channel.parent)[0]))
                    {
                        e.Channel.SendMessage("Goodbye.");
                        Process.GetCurrentProcess().Kill();
                    }
                    else
                    {
                        e.Channel.SendMessage("You have no authority over me.");
                    }
                    return;
                }
                #endregion
                #region Login
                if (message.Contains("login"))
                {
                    Account a = new Account();
                    a.Login(e.author);
                    if (a.profile == null)
                    {
                        a.Create(e.author);
                        Console.WriteLine("Overridden account " + e.author.Username);
                        e.Channel.SendMessage("Created account " + e.author.Username);
                    }
                    else
                    {
                        if (message.Contains("-new"))
                        {
                            a.Create(e.author);
                            Console.WriteLine("Overridden account " + e.author.Username);
                            e.Channel.SendMessage("Overridden account: " + e.author.Username);
                        }
                    }
                    Console.WriteLine("Logged in: " + e.author.Username);
                    e.Channel.SendMessage("Logged in: " + e.author.Username);
                    return;
                }
                #endregion
                if (account.isLoggedIn(e.author))
                {
                    #region Account Options
                    if (message == "profile")
                    {
                        if (account.GetProfile(e.author) != "")
                        {
                            e.Channel.SendMessage(account.GetProfile(e.author));
                        }
                    }
                    if (message == "logout")
                    {
                        if (account.isLoggedIn(e.author))
                        {
                            account.GetAccountFromMember(e.author).Logout(e.author);
                            Console.WriteLine("Logged out: " + e.author.Username);
                            e.Channel.SendMessage("Logged out: " + e.author.Username);
                        }
                        else
                        {
                            e.Channel.SendMessage("Log in before logging out...");
                        }
                    }
                    #endregion
                    if (message.StartsWith("duel "))
                    {
                        Console.WriteLine(e.author.Username + " used duel");
                        string response = message.Split(' ')[1];
                        if (response.Contains("accept"))
                        {
                            Console.WriteLine(e.author.Username + " accepted it");
                            if (battle.isWaitingForInvite())
                            {
                                Console.WriteLine(e.author.Username + " stuff after accept");
                                battle.AcceptBattle(account.GetAccountFromMember(e.author));
                            }
                        }
                        else if(response.Contains("refuse"))
                        {
                            Console.WriteLine(e.author.Username + " refused it");
                            if (battle.isWaitingForInvite())
                            {
                                battle.RefuseBattle(account.GetAccountFromMember(e.author));
                            }
                        }
                    }

                    if (battle.isBattling())
                    {
                        #region Move
                        if (message.StartsWith("move "))
                        {
                            if (battle.canAttack(e.author))
                            {
                                if (message.Split(' ')[1] == "attack")
                                {
                                    DiscordMemberHandler h = new DiscordMemberHandler();
                                    battle.Attack(account.GetAccountFromMember(e.author), account.GetAccountFromID(h.GetIDFromLink(e.message_text, 2)));
                                }
                            }
                        }
                        #endregion
                        #region battle options
                        if (message == "joinbattle")
                        {
                            if (battle.allowJoin)
                            {
                                if (battle.getBattleID(e.author) == -1)
                                {
                                    battle.JoinBattle(account.GetAccountFromMember(e.author));
                                }
                            }
                            else
                            {
                                e.Channel.SendMessage("Sorry, i cannot allow you to join in a duel.");
                            }
                        }
                        if (message == "force-endbattle")
                        {
                            if (account.GetAccountFromMember(e.author).isDeveloper)
                            {
                                battle.EndBattle();
                                e.Channel.SendMessage("Force closed battle");
                            }
                            else
                            {
                                e.Channel.SendMessage("Sorry, only Developers can execute this command.");
                            }
                        }
                        #endregion
                    }
                    else
                    {
                        if (message.StartsWith("startbattle "))
                        {
                            DiscordMemberHandler h = new DiscordMemberHandler();
                            battle.StartBattle(new Account[] { account.GetAccountFromMember(e.author), account.GetAccountFromMember(h.GetMemberFromLink(e.message_text)) }, !message.Contains("-duel"));
                        }
                    }
                }
                else
                {
                    e.Channel.SendMessage("You're not logged in, $login please.");
                }
            }
        }
    }
}