using DiscordSharp;
using Miki.Accounts;
using Miki.Core.Config;
using Miki.Core.Debug;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading;

namespace Miki.Core
{
    /// <summary>
    /// This is our main class. It'll run all the core algorithms.
    /// </summary>
    public class Discord
    {
        public static DiscordClient client = new DiscordClient("MTYwMTA1OTk0MjE3NTg2Njg5.Ce8QnQ.YoAWdFbFrCZ3-i9bkKIkDrmvFek", true, true);
        public static Discord instance;
        public static DateTime timeSinceReset;
        public static AccountManager account = new AccountManager();
        public static Blacklist blacklist = new Blacklist();
        public static ConfigManager config = new ConfigManager();

        /// <summary>
        /// The program runs all discord services and loads all the data here.
        /// </summary>
        public void Start()
        {
            Console.WriteLine("Starting Mikibot v" + Global.VersionText);
            config.Initialize();
            instance = this;
            client.Connected += (sender, e) => { OnConnect(e); };
            client.SocketClosed += (sender, e) => { OnDisconnect(e); };
            client.UserAddedToServer += (sender, e) => { OnUserAdded(e); };
            client.MessageReceived += (sender, e) => { OnMessage(e); };
            client.SendLoginRequest();
            client.Connect();
            Console.ReadLine();
        }

        /// <summary>
        /// This is a event. It'll run whenever the BOT connects to discord.
        /// </summary>
        /// <param name="e">Data recieved from discord when connecting</param>
        public void OnConnect(DiscordConnectEventArgs e)
        {
            Console.WriteLine("Connected! User: " + e.User.Username);
            config.OnConnectInitialize();
            timeSinceReset = DateTime.Now;
            Thread t = new Thread(account.SaveAllAccounts, 0);
            t.Start();
        }

        /// <summary>
        /// This is an event. It'll run when the BOT either crashes or when the internet gets disconnected.  
        /// </summary>
        /// <param name="e">Discord data recieved about error details</param>
        public void OnDisconnect(DiscordSocketClosedEventArgs e)
        {
            Log.Error(e.Code + " - " + e.Reason);
            Start();
        }

        /// <summary>
        /// Event Listener: Gets called whenever a DiscordMember gets added in the server.
        /// </summary>
        /// <param name="e">Data recieved from the DiscordMember that got added.</param>
        public void OnUserAdded(DiscordSharp.Events.DiscordGuildMemberAddEventArgs e)
        {        }

        /// <summary>
        ///  Event Listener: Gets called whenever a message gets recieved by Miki
        /// </summary>
        /// <param name="e">Message data recieved from discord</param>
        public void OnMessage(DiscordSharp.Events.DiscordMessageEventArgs e)
        {
            ChannelMessage channel = new ChannelMessage(e);
            Thread t = new Thread(channel.RecieveMessage, 0);
            if (!blacklist.isBlacklisted(e.Channel.ID))
            {
                if (account.GetAccountFromMember(e.Author) == null)
                {
                    Account a = new Account();
                    a.Login(e.Author);
                    account.AddAccount(a);
                }
                account.GetAccountFromID(e.Author.ID).OnMessageRecieved(e.Channel);
            }
            t.Start();
        }
    }
}