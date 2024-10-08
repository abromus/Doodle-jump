﻿using System;
using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Platforms;

namespace DoodleJump.Game.Worlds
{
    internal interface IPlatformStorage : IDestroyable
    {
        public float HighestPlatformY { get; }

        public IReadOnlyList<IPlatform> Platforms { get; }

        public event Action<IProgressInfo, IPlatformCollisionInfo> Collided;

        public void Clear();

        public void GenerateStartPlatform();

        public void GeneratePlatforms();

        public void DestroyPlatform(IPlatform platform);
    }
}
