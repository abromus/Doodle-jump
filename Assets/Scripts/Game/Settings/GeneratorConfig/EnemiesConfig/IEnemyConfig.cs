using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Settings
{
    internal interface IEnemyConfig : IProbable
    {
        public string Title { get; }

        public BaseEnemy EnemyPrefab { get; }

        public EnemyTriggerType TriggerType { get; }
    }
}
