﻿using DoodleJump.Game.Worlds;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [System.Serializable]
    internal struct StonePlatformConfig : IPlatformConfig, IJumpConfig
    {
        [SerializeField] private Platform _platformPrefab;
        [SerializeField] private float _spawnChance;
        [SerializeField] private TriggerType _triggerType;
        [SerializeField] private int _jumpForce;

        public readonly string Title => "Конфиг каменной платформы";

        public readonly Platform PlatformPrefab => _platformPrefab;

        public readonly float SpawnChance => _spawnChance;

        public readonly TriggerType TriggerType => _triggerType;

        public readonly float JumpForce => _jumpForce;
    }
}