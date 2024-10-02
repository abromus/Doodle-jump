namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class DestroyedPlatformCollisionInfo : IPlatformCollisionInfo
    {
        private readonly IPlatform _platform;

        public IPlatform Platform => _platform;

        internal DestroyedPlatformCollisionInfo(IPlatform platform)
        {
            _platform = platform;
        }
    }
}
