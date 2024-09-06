namespace DoodleJump.Game.Worlds.Platforms
{
    internal interface IPlatformTrigger
    {
        public PlatformTriggerType TriggerType { get; }

        public void Execute();

        public void UpdateInfo(IPlatformCollisionInfo info);
    }
}
