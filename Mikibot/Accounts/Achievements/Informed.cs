namespace Miki.Accounts.Achievements
{
    internal class Informed : Achievement
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