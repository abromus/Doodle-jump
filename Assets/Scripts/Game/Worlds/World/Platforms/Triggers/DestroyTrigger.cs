namespace DoodleJump.Game.Worlds.Platforms
{
    internal readonly struct DestroyTrigger : IPlatformTrigger
    {
        private readonly IPlatform _platform;

        public readonly PlatformTriggerType TriggerType => PlatformTriggerType.Destroy;

        public DestroyTrigger(IPlatform platform)
        {
            _platform = platform;
        }

        public readonly void Execute()
        {
            _platform.Destroy();
        }

        public void UpdateInfo(IPlatformCollisionInfo info) { }
    }
}
