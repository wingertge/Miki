using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Miki.Accounts.Achievements
{
    class Informed:Achievement
    {
        public override void Initialize(Account a)
        {
            icon = ":books:";
            name = "informed";
            base.Initialize(a);
        }

        public override void UpdateProgress()
        {
            OnAchievementGet();
        }

        public override void OnAchievementGet()
        {
            base.OnAchievementGet();
        }
    }
}
