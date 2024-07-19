﻿using DoodleJump.Game.Worlds;

namespace DoodleJump.Game.Settings
{
    internal interface IPlatformConfig
    {
        public string Title { get; }

        public Platform PlatformPrefab { get; }

        public float SpawnChance { get; }

        public TriggerType TriggerType { get; }
    }
}
