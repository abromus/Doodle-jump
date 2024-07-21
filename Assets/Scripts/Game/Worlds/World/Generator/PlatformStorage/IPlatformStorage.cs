using System;
using System.Collections.Generic;
using DoodleJump.Core;

namespace DoodleJump.Game.Worlds
{
    internal interface IPlatformStorage : IDestroyable
    {
        public float HighestPlatformY { get; }

        public IReadOnlyList<IPlatform> Platforms { get; }

        public event Action<IPlatformCollisionInfo> Collided;

        public void Clear();

        public void GenerateStartPlatform();

        public void TryGeneratePlatform();

        public void GeneratePlatforms();

        public void DestroyPlatform(IPlatform platform);
    }
}
