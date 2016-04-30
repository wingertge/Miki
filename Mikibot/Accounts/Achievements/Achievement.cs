namespace Miki.Accounts
{
    public class Achievement
    {
        public string icon;
        public string name;
        public string description;
        public bool isGoingToAchieve;
        public bool hasAchieved;

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
            if (isGoingToAchieve && !hasAchieved)
            {
                hasAchieved = true;
                account.GetChannel().SendMessage(":confetti_ball:  " + account.GetMember(account.memberID).Username + " gained the achievement: " + name + " :confetti_ball:");
            }
            isGoingToAchieve = false;
        }

        public void LoadAchievement(bool alreadyAchieved)
        {
            hasAchieved = alreadyAchieved;
        }

        public string GetAchievement()
        {
            return icon + " " + name;
        }
    }
}