﻿using DoodleJump.Game.Worlds.Platforms;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct EarthSpringPlatformConfig : IPlatformConfig, ISpringJumpConfig
    {
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private PlatformTriggerType _triggerType;
        [SerializeField] private float _defaultJumpForce;
        [SerializeField] private float _springJumpForce;

        public readonly string Title => "Конфиг земляной платформы с пружиной";

        public readonly Platform PlatformPrefab => _platformPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly PlatformTriggerType TriggerType => _triggerType;

        public readonly float JumpForce => _defaultJumpForce;

        public readonly float SpringJumpForce => _springJumpForce;

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
