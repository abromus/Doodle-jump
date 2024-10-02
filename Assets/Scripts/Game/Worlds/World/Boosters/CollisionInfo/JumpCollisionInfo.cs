namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class JumpCollisionInfo : IBoosterCollisionInfo
    {
        private readonly IWorldBooster _worldBooster;
        private readonly Settings.IJumpBoosterConfig _config;

        public IWorldBooster WorldBooster => _worldBooster;

        public Settings.IBoosterConfig Config => _config;

        internal JumpCollisionInfo(IWorldBooster worldBooster, Settings.IJumpBoosterConfig config)
        {
            _worldBooster = worldBooster;
            _config = config;
        }
    }
}
