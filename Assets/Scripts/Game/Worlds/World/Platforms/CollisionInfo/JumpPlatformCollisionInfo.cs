namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class JumpPlatformCollisionInfo : IPlatformCollisionInfo
    {
        private readonly IPlatform _platform;

        public IPlatform Platform => _platform;

        internal JumpPlatformCollisionInfo(IPlatform platform)
        {
            _platform = platform;
        }
    }
}
