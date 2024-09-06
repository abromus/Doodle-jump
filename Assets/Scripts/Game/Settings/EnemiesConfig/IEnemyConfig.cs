using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Settings
{
    internal interface IEnemyConfig
    {
        public string Title { get; }

        public Enemy EnemyPrefab { get; }

        public float SpawnChance { get; }

        public EnemyTriggerType TriggerType { get; }

#if UNITY_EDITOR
        public void ChangeSpawnChance(float factor);
#endif
    }
}
