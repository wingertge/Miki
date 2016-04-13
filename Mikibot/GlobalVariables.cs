using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki
{
    public class GlobalVariables
    {
        public static string AccountsFolder = @"C:/" + Environment.SpecialFolder.ApplicationData + "/MikiBot/accounts/";

        public enum Roles { BOT, Playing, InBattle };
    }
}
