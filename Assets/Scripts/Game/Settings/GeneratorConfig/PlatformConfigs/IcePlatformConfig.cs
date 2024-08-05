using DoodleJump.Game.Worlds;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct IcePlatformConfig : IPlatformConfig, IJumpConfig
    {
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private TriggerType _triggerType;
        [SerializeField] private float _jumpForce;

        public readonly string Title => "Конфиг ледяной платформы";

        public readonly Platform PlatformPrefab => _platformPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly TriggerType TriggerType => _triggerType;

        public readonly float JumpForce => _jumpForce;

#if UNITY_EDITOR
        public void ChangeSpawnChance(float factor)
        {
            _spawnChance *= factor;
        }
#endif
    }
}
