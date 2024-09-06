namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DestroyTrigger : IEnemyTrigger
    {
        private readonly IWorldData _worldData;
        private readonly IEnemy _enemy;

        public readonly EnemyTriggerType TriggerType => EnemyTriggerType.Destroy;

        public DestroyTrigger(IWorldData worldData, IEnemy enemy)
        {
            _worldData = worldData;
            _enemy = enemy;
        }

        public readonly void Execute()
        {
            _enemy.Destroy();
            _worldData.Restart();
        }

        public void UpdateInfo(IEnemyCollisionInfo info) { }
    }
}
