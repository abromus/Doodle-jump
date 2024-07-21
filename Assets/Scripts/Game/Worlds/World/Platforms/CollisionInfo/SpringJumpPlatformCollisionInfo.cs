namespace DoodleJump.Game.Worlds
{
    internal sealed class SpringJumpPlatformCollisionInfo : IPlatformCollisionInfo
    {
        private bool _isSpringCollided;

        private readonly IPlatform _platform;

        public IPlatform Platform => _platform;

        public bool IsSpringCollided => _isSpringCollided;

        public SpringJumpPlatformCollisionInfo(IPlatform platform)
        {
            _platform = platform;
        }

        public void SetIsSpringCollided(bool isSpringCollided)
        {
            _isSpringCollided = isSpringCollided;
        }
    }
}
