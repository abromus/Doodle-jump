namespace DoodleJump.Game.Worlds
{
    internal sealed class JumpPlatformCollisionInfo : IPlatformCollisionInfo
    {
        private readonly IPlatform _platform;

        public IPlatform Platform => _platform;

        public JumpPlatformCollisionInfo(IPlatform platform)
        {
            _platform = platform;
        }
    }
}
