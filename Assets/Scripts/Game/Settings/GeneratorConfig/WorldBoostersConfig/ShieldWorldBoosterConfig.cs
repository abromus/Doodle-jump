using DoodleJump.Game.Worlds.Boosters;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct ShieldWorldBoosterConfig : IWorldBoosterConfig
    {
        [SerializeField] private WorldBooster _worldBoosterPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private BoosterTriggerType _triggerType;

        public readonly string Title => "������ ����";

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

            UnityEditor.AssetDatabase.SaveAssets();
            UnityEditor.AssetDatabase.Refresh();
        }
#endif
    }
}
