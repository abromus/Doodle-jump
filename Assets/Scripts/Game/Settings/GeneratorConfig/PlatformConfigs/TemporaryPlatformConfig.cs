using DoodleJump.Game.Worlds.Platforms;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct TemporaryPlatformConfig : IPlatformConfig, ITemporaryConfig
    {
        [SerializeField] private BasePlatform _platformPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private PlatformTriggerType _triggerType;
        [SerializeField] private float _jumpForce;
        [SerializeField] private float _existenceTime;

        public readonly string Title => "Конфиг исчезающей по времени платформы";

        public readonly BasePlatform PlatformPrefab => _platformPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly PlatformTriggerType TriggerType => _triggerType;

        public readonly float JumpForce => _jumpForce;

        public readonly float ExistenceTime => _existenceTime;

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
