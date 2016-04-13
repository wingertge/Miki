using DiscordSharp.Events;
using Miki.IMDb;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;


namespace Miki.Core
{
    class ChannelMessage
    {
        DiscordMessageEventArgs e;

        public ChannelMessage(DiscordMessageEventArgs e)
        {
            this.e = e;
        }

        public void RecieveMessage()
        {
            string message = e.MessageText.Trim(new char[] { '>' });
            message = message.ToLower();

            if (e.MessageText.StartsWith(">"))
            {
                if(message == "help")
                {
                    e.Channel.SendMessage("Public Commands:\n>imdb <title> - gets you some cool information from the imdb website\n>profile - check your level and experience!");
                }
                if (message.StartsWith("imdb "))
                {
                    IMDbNet_Discord imdb = new IMDbNet_Discord();
                    imdb.channel = e.Channel;
                    string messageTrimmed = message.Substring(5);
                    imdb.movieTitle = messageTrimmed;
                    Thread t = new Thread(new ThreadStart(imdb.GetData), 0);
                    t.Start();
                    return;
                }
                if(message == "uptime")
                {
                    TimeSpan t = DateTime.Now.Subtract(Discord.timeSinceReset);
                    e.Channel.SendMessage(t.ToString());
                }
                if (message == "stats")
                {
                    Process proc = Process.GetCurrentProcess();
                    int memsize = 0;
                    PerformanceCounter PC = new PerformanceCounter();
                    PC.CategoryName = "Process";
                    PC.CounterName = "Working Set - Private";
                    PC.InstanceName = proc.ProcessName;
                    memsize = Convert.ToInt32(PC.NextValue()) / (int)(1024) / (int)1024;
                    PC.Close();
                    PC.Dispose();
                    int members = 0;
                    for(int i =0; i < Discord.client.GetServersList().Count; i++)
                    {
                        members += Discord.client.GetServersList()[i].Members.Count;
                    }
                    e.Channel.SendMessage("running on **" + Discord.client.GetServersList().Count + " servers**\nusing **" + memsize + "MB** ram\nused by **" + members + " users**");
                }
                if (message.StartsWith("fizzbuzz "))
                {
                    int input = 0;
                    try
                    {
                        input = int.Parse(message.Split(' ')[1]);
                        if (input == 0)
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
                    if (input % 3 == 0)
                    {
                        output += "fizz";
                    }
                    if (input % 5 == 0)
                    {
                        output += "buzz";
                    }
                    if (output == "")
                    {
                        e.Channel.SendMessage(input.ToString());
                    }
                    else
                    {
                        e.Channel.SendMessage(output);
                    }
                    return;
                }
                if (message == "profile")
                {
                    if(message.StartsWith("profile "))
                    {
                        DiscordMemberHandler m = new DiscordMemberHandler();
                        e.Channel.SendMessage(Discord.account.GetProfile(m.GetMemberFromLink(e.Channel, message)));
                        return;
                    }
                    if (Discord.account.GetProfile(e.Author) != "")
                    {
                        e.Channel.SendMessage(Discord.account.GetProfile(e.Author));
                    }
                }

            }
        }
    }
}
