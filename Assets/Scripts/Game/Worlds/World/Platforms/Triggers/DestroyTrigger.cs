namespace DoodleJump.Game.Worlds.Platforms
{
    internal readonly struct DestroyTrigger : IPlatformTrigger
    {
        private readonly IPlatform _platform;

        public readonly PlatformTriggerType TriggerType => PlatformTriggerType.Destroy;

        internal DestroyTrigger(IPlatform platform)
        {
            _platform = platform;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public readonly void Execute()
        {
            _platform.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateInfo(IPlatformCollisionInfo info) { }
    }
}
