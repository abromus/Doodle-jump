using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct FlyingSaucerConfig : IEnemyConfig
    {
        [SerializeField] private Enemy _enemyPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private EnemyTriggerType _triggerType;

        public readonly string Title => "Конфиг летающей тарелки";

        public readonly Enemy EnemyPrefab => _enemyPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly EnemyTriggerType TriggerType => _triggerType;

#if UNITY_EDITOR
        public void ChangeSpawnChance(float factor)
        {
            _spawnChance *= factor;
        }
#endif
    }
}
