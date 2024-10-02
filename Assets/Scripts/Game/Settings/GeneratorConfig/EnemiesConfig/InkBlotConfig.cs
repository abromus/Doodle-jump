using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct InkBlotConfig : IEnemyConfig
    {
        [SerializeField] private BaseEnemy _enemyPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private EnemyTriggerType _triggerType;

        public readonly string Title => "Конфиг кляксы";

        public readonly BaseEnemy EnemyPrefab => _enemyPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly EnemyTriggerType TriggerType => _triggerType;

#if UNITY_EDITOR
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void ChangeSpawnChance(float factor)
        {
            _spawnChance *= factor;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetSpawnChance(float chance)
        {
            _spawnChance = chance;
        }
#endif
    }
}
