using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Core
{
    public class Global
    {
        public const string MikiFolder = @"/miki/";

        public static string AccountsFolder = MikiFolder + "accounts/";
        public static string AvatarsFolder = MikiFolder + "avatars/";
        public static string ConfigFile = MikiFolder + "config.cfg";

        public const string VersionNumber = "0.0.9";
        public static string VersionText = (Debugger.IsAttached) ? VersionNumber + "_beta" : VersionNumber;
    }
}
