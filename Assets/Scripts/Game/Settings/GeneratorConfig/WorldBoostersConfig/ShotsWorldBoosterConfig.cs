using DoodleJump.Game.Worlds.Boosters;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct ShotsWorldBoosterConfig : IWorldBoosterConfig
    {
        [SerializeField] private BaseWorldBooster _worldBoosterPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private BoosterTriggerType _triggerType;

        public readonly string Title => "Конфиг снарядов";

        public readonly BaseWorldBooster WorldBoosterPrefab => _worldBoosterPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly BoosterTriggerType TriggerType => _triggerType;

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
