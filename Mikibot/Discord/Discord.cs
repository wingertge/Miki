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
using System.Threading;
using System.Threading.Tasks;

namespace Miki
{
    class Discord
    {
        public static DiscordClient client = new DiscordClient("MTYwMTA1OTk0MjE3NTg2Njg5.Ce8QnQ.YoAWdFbFrCZ3-i9bkKIkDrmvFek", true, true);
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
            Console.WriteLine("Connected! User: " + e.User.Username);
            client.UpdateCurrentGame("Mikibot v" + Program.VersionNumber);
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
            Console.WriteLine(e.Author.Username + " used duel");
            string response = e.message;
            if (response.Contains("accept"))
            {
                Console.WriteLine(e.Author.Username + " accepted it");
                if (battle.isWaitingForInvite())
                {
                    Console.WriteLine(e.Author.Username + " stuff after accept");
                    battle.AcceptBattle(account.GetAccountFromMember(e.Author));
                }
            }
            else if (response.Contains("refuse"))
            {
                Console.WriteLine(e.Author.Username + " refused it");
                if (battle.isWaitingForInvite())
                {
                    battle.RefuseBattle(account.GetAccountFromMember(e.Author));
                }
            }*/
        }

        /* Event Listener: Gets called whenever a message gets recieved by Miki */
        public void OnMessage(DiscordSharp.Events.DiscordMessageEventArgs e)
        {
            string message = e.MessageText.Trim(new char[] { '$' });
            message = message.ToLower();

            if (e.MessageText.StartsWith("$"))
            {
                #region Standard bot information
                if(message.StartsWith("imdb "))
                {
                    IMDbNet_Discord imdb = new IMDbNet_Discord();
                    imdb.channel = e.Channel;
                    string messageTrimmed = message.Substring(5);
                    imdb.movieTitle = messageTrimmed;
                    Thread t = new Thread(new ThreadStart(imdb.GetData), 0);
                    t.Start();
                    return;
                }
                if(message =="stats")
                {
                    Process proc = Process.GetCurrentProcess();

                    int memsize = 0; // memsize in Megabyte
                    PerformanceCounter PC = new PerformanceCounter();
                    PC.CategoryName = "Process";
                    PC.CounterName = "Working Set - Private";
                    PC.InstanceName = proc.ProcessName;
                    memsize = Convert.ToInt32(PC.NextValue()) / (int)(1024) / (int)1024;
                    PC.Close();
                    PC.Dispose();
                    e.Channel.SendMessage("running on " + client.GetServersList().Count + " servers\nusing " + memsize + "MB ram");

                }
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

                }
                #endregion
                #region Login
                if (message.Contains("login"))
                {
                    Account a = new Account();
                    a.Login(e.Author);
                    if (a.profile == null)
                    {
                        a.Create(e.Author);
                        Console.WriteLine("Overridden account " + e.Author.Username);
                        e.Channel.SendMessage("Created account " + e.Author.Username);
                    }
                    else
                    {
                        if (message.Contains("-new"))
                        {
                            a.Create(e.Author);
                            Console.WriteLine("Overridden account " + e.Author.Username);
                            e.Channel.SendMessage("Overridden account: " + e.Author.Username);
                        }
                    }
                    Console.WriteLine("Logged in: " + e.Author.Username);
                    e.Channel.SendMessage("Logged in: " + e.Author.Username);
                    return;
                }
                #endregion
                if (account.isLoggedIn(e.Author))
                {
                    #region Account Options
                    if (message == "profile")
                    {
                        if (account.GetProfile(e.Author) != "")
                        {
                            e.Channel.SendMessage(account.GetProfile(e.Author));
                        }
                    }
                    if (message == "logout")
                    {
                        if (account.isLoggedIn(e.Author))
                        {
                            account.GetAccountFromMember(e.Author).Logout(e.Author);
                            Console.WriteLine("Logged out: " + e.Author.Username);
                            e.Channel.SendMessage("Logged out: " + e.Author.Username);
                        }
                        else
                        {
                            e.Channel.SendMessage("Log in before logging out...");
                        }
                    }
                    #endregion
                    if (message.StartsWith("duel "))
                    {
                        Console.WriteLine(e.Author.Username + " used duel");
                        string response = message.Split(' ')[1];
                        if (response.Contains("accept"))
                        {
                            Console.WriteLine(e.Author.Username + " accepted it");
                            if (battle.isWaitingForInvite())
                            {
                                Console.WriteLine(e.Author.Username + " stuff after accept");
                                battle.AcceptBattle(account.GetAccountFromMember(e.Author));
                            }
                        }
                        else if(response.Contains("refuse"))
                        {
                            Console.WriteLine(e.Author.Username + " refused it");
                            if (battle.isWaitingForInvite())
                            {
                                battle.RefuseBattle(account.GetAccountFromMember(e.Author));
                            }
                        }
                    }

                    if (battle.isBattling())
                    {
                        #region Move
                        if (message.StartsWith("move "))
                        {
                            if (battle.canAttack(e.Author))
                            {
                                if (message.Split(' ')[1] == "attack")
                                {
                                    DiscordMemberHandler h = new DiscordMemberHandler();
                                    battle.Attack(account.GetAccountFromMember(e.Author), account.GetAccountFromID(h.GetIDFromLink(e.MessageText, 2)));
                                }
                            }
                        }
                        #endregion
                        #region battle options
                        if (message == "joinbattle")
                        {
                            if (battle.allowJoin)
                            {
                                if (battle.getBattleID(e.Author) == -1)
                                {
                                    battle.JoinBattle(account.GetAccountFromMember(e.Author));
                                }
                            }
                            else
                            {
                                e.Channel.SendMessage("Sorry, i cannot allow you to join in a duel.");
                            }
                        }
                        if (message == "force-endbattle")
                        {
                            if (account.GetAccountFromMember(e.Author).isDeveloper)
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
                            battle.StartBattle(new Account[] { account.GetAccountFromMember(e.Author), account.GetAccountFromMember(h.GetMemberFromLink(e.MessageText)) }, !message.Contains("-duel"));
                        }
                    }
                }
                else
                {
                 //   e.Channel.SendMessage("You're not logged in, $login please.");
                }
            }
        }
    }
}