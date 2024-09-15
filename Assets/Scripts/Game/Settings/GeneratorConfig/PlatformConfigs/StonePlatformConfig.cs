﻿using DoodleJump.Game.Worlds.Platforms;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct StonePlatformConfig : IPlatformConfig, IJumpConfig
    {
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private PlatformTriggerType _triggerType;
        [SerializeField] private float _jumpForce;

        public readonly string Title => "Конфиг каменной платформы";

        public readonly Platform PlatformPrefab => _platformPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly PlatformTriggerType TriggerType => _triggerType;

        public readonly float JumpForce => _jumpForce;

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
