namespace Miki.Accounts.Achievements
{
    internal class Level5 : Achievement
    {
        public override void Initialize(Account a)
        {
            base.Initialize(a);
        }

        public override void UpdateProgress()
        {
            if (account.GetLevel() >= 5)
            {
                OnAchievementGet();
            }
        }

        public override void OnAchievementGet()
        {
            base.OnAchievementGet();
        }
    }
}