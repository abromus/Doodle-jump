using DoodleJump.Game.Worlds.Boosters;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct MoneyWorldBoosterConfig : IWorldBoosterConfig
    {
        [SerializeField] private WorldBooster _worldBoosterPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private BoosterTriggerType _triggerType;

        public readonly string Title => "Конфиг монеты";

        public readonly WorldBooster WorldBoosterPrefab => _worldBoosterPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly BoosterTriggerType TriggerType => _triggerType;

#if UNITY_EDITOR
        public void ChangeSpawnChance(float factor)
        {
            _spawnChance *= factor;
        }

        public void SetSpawnChance(float chance)
        {
            _spawnChance = chance;
        }
#endif
    }
}
