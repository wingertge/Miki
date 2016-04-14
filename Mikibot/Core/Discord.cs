using DiscordSharp;
using Miki.Accounts;
using Miki.Core.Config;
using Miki.Core.Debug;
using System;
using System.Diagnostics;
using System.Threading;

namespace Miki.Core
{
    public class Discord
    {
        public static DiscordClient client = new DiscordClient("MTYwMTA1OTk0MjE3NTg2Njg5.Ce8QnQ.YoAWdFbFrCZ3-i9bkKIkDrmvFek", true, true);
        public static Discord instance;
        public static DateTime timeSinceReset;

        public static AccountManager account = new AccountManager();
        public static ConfigManager config = new ConfigManager();

        /* Start of program, gets called from Program.Main() */
        public void Start()
        {
            Console.WriteLine("Starting Mikibot v" + Global.VersionNumber);
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

        /* Event Listener: Gets called when Miki connects to the server */
        public void OnConnect(DiscordConnectEventArgs e)
        {
            Console.WriteLine("Connected! User: " + e.User.Username);
            client.UpdateCurrentGame("'>help' for help");
            timeSinceReset = DateTime.Now;
            Thread t = new Thread(account.SaveAllAccounts, 0);
            t.Start();
        }

        public void OnDisconnect(DiscordSocketClosedEventArgs e)
        {
            Log.Error(e.Code + " - " + e.Reason);
        }

        /* Event Listener: Gets called whenever a DiscordMember gets added in the server. */
        public void OnUserAdded(DiscordSharp.Events.DiscordGuildMemberAddEventArgs e)
        {
            e.AddedMember.SendMessage("Welcome to Miki " + Global.VersionNumber +"\nTry '$login' to start!." );
        }

        /* Event Listener: Gets called whenever a message gets recieved by Miki */
        public void OnMessage(DiscordSharp.Events.DiscordMessageEventArgs e)
        {
            ChannelMessage channel = new ChannelMessage(e);
            Thread t = new Thread(channel.RecieveMessage, 0);
            if(account.GetAccountFromMember(e.Author) == null)
            {
                Account a = new Account();
                a.Login(e.Author);
                account.AddAccount(a);
            }
            account.GetAccountFromID(e.Author.ID).OnMessageRecieved(e.Channel);
            t.Start();
        }
    }
}