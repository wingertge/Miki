using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Accounts
{
    class Achievement
    {
        public string name;
        public string description;

        protected bool hasAchieved;
        protected Account account;

        public virtual void Initialize(Account a)
        {
            account = a;
        }

        public virtual void UpdateProgress()
        {

        }

        public virtual void OnAchievementGet()
        {
            if (!hasAchieved)
            {
                hasAchieved = true;
                account.profile.lastActiveChannel.SendMessage(":confetti_ball:  " + account.member.Username + " gained the achievement: " + name + " :confetti_ball:");
            }
        }
    }
}
