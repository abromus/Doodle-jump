namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class SpringJumpPlatformCollisionInfo : IPlatformCollisionInfo
    {
        private bool _isSpringCollided;

        private readonly IPlatform _platform;

        public IPlatform Platform => _platform;

        public bool IsSpringCollided => _isSpringCollided;

        internal SpringJumpPlatformCollisionInfo(IPlatform platform)
        {
            _platform = platform;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetIsSpringCollided(bool isSpringCollided)
        {
            _isSpringCollided = isSpringCollided;
        }
    }
}
