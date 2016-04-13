using Miki.Core;

namespace Miki
{
    public class Program
    {
        public const string VersionNumber = "0.0.8";
        static Discord d = new Discord();

        static void Main(string[] args)
        {
            d.Start();
        }
    }
}
