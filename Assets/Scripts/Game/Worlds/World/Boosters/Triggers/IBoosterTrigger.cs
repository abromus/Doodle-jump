namespace DoodleJump.Game.Worlds.Boosters
{
    internal interface IBoosterTrigger
    {
        public BoosterTriggerType TriggerType { get; }

        public void Execute();

        public void UpdateInfo(IBoosterCollisionInfo info);
    }
}
