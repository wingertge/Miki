using Miki.Accounts.Achievements;
using Miki.Core;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Accounts.Profiles
{
    public class AchievementsManager
    {
        Account parent;

        List<Achievement> achievements = new List<Achievement>();

        public void Initialize(Account a)
        {
            parent = a;
            achievements.Add(new Level5());

            for(int i = 0; i < achievements.Count; i++)
            {
                achievements[i].Initialize(a);
            }
        }

        public void CheckAllAchievements()
        {

        }

        public void SaveProfile(string id)
        {
            if (!Directory.Exists(Global.AccountsFolder + id))
            {
                Directory.CreateDirectory(Global.AccountsFolder + id);
            }
            StreamWriter sw = new StreamWriter(Global.AccountsFolder + id + ".profile");
            sw.Close();
        }
        public void LoadProfile(string id)
        {
            if (!Directory.Exists(Global.AccountsFolder + id))
            {
                return;
            }
            StreamReader sr = new StreamReader(Global.AccountsFolder + id + ".profile");
            sr.Close();
        }
    }
}
