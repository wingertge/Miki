using Miki.Core;
using Miki.Core.Debug;
using System.Diagnostics;

namespace Miki
{
    public class Program
    {
        static Discord d = new Discord();

        static void Main(string[] args)
        {
            if(args.Length > 0 || Debugger.IsAttached)
            {
                Global.Debug = true;
                Log.Warning("Debug mode");
            }
            d.Start();
        }
    }
}
