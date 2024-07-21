using DoodleJump.Game.Worlds;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct EarthSpringPlatformConfig : IPlatformConfig, ISpringJumpConfig
    {
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private TriggerType _triggerType;
        [SerializeField] private int _defaultJumpForce;
        [SerializeField] private int _springJumpForce;

        public readonly string Title => "Конфиг земляной платформы с пружиной";

        public readonly Platform PlatformPrefab => _platformPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly TriggerType TriggerType => _triggerType;

        public readonly float JumpForce => _defaultJumpForce;

        public readonly float SpringJumpForce => _springJumpForce;

#if UNITY_EDITOR
        public void ChangeSpawnChance(float factor)
        {
            _spawnChance *= factor;
        }
#endif
    }
}
