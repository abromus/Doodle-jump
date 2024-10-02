namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class MoneyCollisionInfo : IBoosterCollisionInfo
    {
        private readonly IWorldBooster _worldBooster;
        private readonly Settings.IMoneyBoosterConfig _config;

        public IWorldBooster WorldBooster => _worldBooster;

        public Settings.IBoosterConfig Config => _config;

        internal MoneyCollisionInfo(IWorldBooster worldBooster, Settings.IMoneyBoosterConfig config)
        {
            _worldBooster = worldBooster;
            _config = config;
        }
    }
}
