using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Accounts.Profiles.Achievements
{
    class SendOneMessage:Achievement
    {
        public override void Initialize(Account a)
        {
            base.Initialize(a);
        }

        public override void UpdateProgress()
        {
            OnAchievementGet();
        }
    }
}
