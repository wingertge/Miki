using DiscordSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace MikiBot
{
    public class Program
    {
        public const string VersionNumber = "0.0.5";
        static Discord d = new Discord();

        static void Main(string[] args)
        {
            d.Start();
        }
    }
}
