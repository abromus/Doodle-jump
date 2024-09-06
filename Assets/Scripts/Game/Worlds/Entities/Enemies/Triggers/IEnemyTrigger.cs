namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IEnemyTrigger
    {
        public EnemyTriggerType TriggerType { get; }

        public void Execute();

        public void UpdateInfo(IEnemyCollisionInfo info);
    }
}
