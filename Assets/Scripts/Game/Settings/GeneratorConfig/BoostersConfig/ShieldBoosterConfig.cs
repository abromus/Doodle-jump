using DoodleJump.Game.Worlds.Boosters;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct ShieldBoosterConfig : IBoosterConfig
    {
        [SerializeField] private Booster _boosterPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private BoosterTriggerType _triggerType;

        public readonly string Title => "Конфиг щита";

        public readonly Booster BoosterPrefab => _boosterPrefab;

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

            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
#endif
    }
}
