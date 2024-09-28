namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class JetpackCollisionInfo : IBoosterCollisionInfo
    {
        private readonly IWorldBooster _worldBooster;
        private readonly Settings.IJetpackBoosterConfig _config;

        public IWorldBooster WorldBooster => _worldBooster;

        public Settings.IBoosterConfig Config => _config;

        public JetpackCollisionInfo(IWorldBooster worldBooster, Settings.IJetpackBoosterConfig config)
        {
            _worldBooster = worldBooster;
            _config = config;
        }
    }
}
