namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class ShotCollisionInfo : IBoosterCollisionInfo
    {
        private readonly IWorldBooster _worldBooster;
        private readonly Settings.IShotBoosterConfig _config;

        public IWorldBooster WorldBooster => _worldBooster;

        public Settings.IBoosterConfig Config => _config;

        public ShotCollisionInfo(IWorldBooster worldBooster, Settings.IShotBoosterConfig config)
        {
            _worldBooster = worldBooster;
            _config = config;
        }
    }
}
