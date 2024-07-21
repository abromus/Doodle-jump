namespace DoodleJump.Game.Worlds
{
    internal readonly struct DestroyTrigger : ITrigger
    {
        private readonly IPlatform _platform;

        public readonly TriggerType TriggerType => TriggerType.Destroy;

        public DestroyTrigger(IPlatform platform)
        {
            _platform = platform;
        }

        public readonly void Execute()
        {
            _platform.Destroy();
        }
    }
}
