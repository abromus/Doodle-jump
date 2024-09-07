using DoodleJump.Game.Worlds.Platforms;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct QuantityPlatformConfig : IPlatformConfig, IQuantityConfig
    {
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private PlatformTriggerType _triggerType;
        [SerializeField] private float _jumpForce;
        [SerializeField] private int _quantity;

        public readonly string Title => "Конфиг исчезающей по количеству платформы";

        public readonly Platform PlatformPrefab => _platformPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly PlatformTriggerType TriggerType => _triggerType;

        public readonly float JumpForce => _jumpForce;

        public readonly int Quantity => _quantity;

#if UNITY_EDITOR
        public void ChangeSpawnChance(float factor)
        {
            _spawnChance *= factor;
        }
#endif
    }
}
