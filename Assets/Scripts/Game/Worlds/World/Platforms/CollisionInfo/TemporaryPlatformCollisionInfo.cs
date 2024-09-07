namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class TemporaryPlatformCollisionInfo : IPlatformCollisionInfo
    {
        private float _existenceTime;

        private readonly IPlatform _platform;

        public IPlatform Platform => _platform;

        public float ExistenceTime => _existenceTime;

        public TemporaryPlatformCollisionInfo(IPlatform platform, float existenceTime)
        {
            _platform = platform;
            _existenceTime = existenceTime;
        }

        public void UpdateTime(float existenceTime)
        {
            _existenceTime = existenceTime;
        }
    }
}
