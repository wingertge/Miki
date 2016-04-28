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

        Dictionary<string, Achievement> achievements = new Dictionary<string, Achievement>();

        public void Initialize(Account a)
        {
            parent = a;
            achievements.Add("informed", new Informed());
            achievements.Add("level5", new Level5());

            foreach (KeyValuePair<string, Achievement> item in achievements)
            {
                item.Value.Initialize(a);
            }
        }

        public void CheckAllAchievements()
        {
            foreach(KeyValuePair<string, Achievement> item in achievements)
            {
                item.Value.UpdateProgress();
            }
        }

        public string PrintAchieved()
        {
            string output = "";
            foreach (KeyValuePair<string, Achievement> item in achievements)
            {
                if(item.Value.hasAchieved)
                {
                    output += item.Value.GetAchievement() + '\n';
                }
            }
            return output;
        }

        public Achievement GetAchievement(string id)
        {
            return achievements[id];
        }

        public void SaveProfile(string id)
        {
            StreamWriter sw = new StreamWriter(Global.AccountsFolder + id + "/" + id + ".achievement");
            foreach (KeyValuePair<string, Achievement> item in achievements)
            {
                sw.WriteLine(item.Key + ":" + item.Value.hasAchieved);
            }
            sw.Close();
        }
        public void LoadProfile(string id)
        {
            if (!File.Exists(Global.AccountsFolder + id + "/" + id + ".achievement"))
            {
                SaveProfile(id);
                return;
            }
            StreamReader sr = new StreamReader(Global.AccountsFolder + id + "/" + id + ".achievement");
            string input = "start";
            while (input != "")
            {
                input = sr.ReadLine();
                if(input == null)
                {
                    break;
                }
                achievements[input.Split(':')[0]].LoadAchievement(bool.Parse(input.Split(':')[1]));
            }
            sr.Close();
        }
    }
}
