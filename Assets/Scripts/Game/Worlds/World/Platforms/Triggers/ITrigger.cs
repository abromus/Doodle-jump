namespace DoodleJump.Game.Worlds
{
    internal interface ITrigger
    {
        public TriggerType TriggerType { get; }

        public void Execute();

        public void UpdateInfo(IPlatformCollisionInfo info);
    }
}
