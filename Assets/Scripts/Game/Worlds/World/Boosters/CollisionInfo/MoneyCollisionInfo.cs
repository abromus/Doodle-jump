namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class MoneyCollisionInfo : IMoneyCollisionInfo
    {
        private readonly IWorldBooster _worldBooster;
        private readonly int _money;

        public IWorldBooster WorldBooster => _worldBooster;

        public Settings.IBoosterConfig Config => null;

        public int Money => _money;

        public MoneyCollisionInfo(IWorldBooster worldBooster, int money)
        {
            _worldBooster = worldBooster;
            _money = money;
        }
    }
}
