namespace DoodleJump.Game.Settings
{
    internal interface IEnemyConfig : IProbable
    {
        public string Title { get; }

        public Worlds.Entities.BaseEnemy EnemyPrefab { get; }

        public Worlds.Entities.EnemyTriggerType TriggerType { get; }
    }
}
