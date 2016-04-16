using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Core
{
    /// <summary>
    /// Global data for folder structures and versioning.
    /// </summary>
    public class Global
    {
        public const string MikiFolder = @"/miki/";

        public static string AccountsFolder = MikiFolder + "accounts/";
        public static string AvatarsFolder = MikiFolder + "avatars/" + AvatarImage;
        public static string AvatarImage = "avatar.png";
        public static string ConfigFile = MikiFolder + "preferences.config";

        public static string Status = "'>help' | " + VersionText;
        public const string VersionNumber = "0.1.1";
        public static string VersionText = (Debugger.IsAttached) ? VersionNumber + "_beta" : VersionNumber;
    }
}
