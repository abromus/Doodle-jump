namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class ShieldCollisionInfo : IBoosterCollisionInfo
    {
        private readonly IWorldBooster _worldBooster;
        private readonly Settings.IShieldBoosterConfig _config;

        public IWorldBooster WorldBooster => _worldBooster;

        public Settings.IBoosterConfig Config => _config;

        public ShieldCollisionInfo(IWorldBooster worldBooster, Settings.IShieldBoosterConfig config)
        {
            _worldBooster = worldBooster;
            _config = config;
        }
    }
}
