namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DestroyTrigger : IEnemyTrigger
    {
        private readonly IWorldData _worldData;
        private readonly IEnemy _enemy;

        public readonly EnemyTriggerType TriggerType => EnemyTriggerType.Destroy;

        internal DestroyTrigger(IWorldData worldData, IEnemy enemy)
        {
            _worldData = worldData;
            _enemy = enemy;
        }

        public readonly void Execute()
        {
            _enemy.Destroy();
            _worldData.GameOver(GameOverType.EnemyCollided);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateInfo(IEnemyCollisionInfo info) { }
    }
}
