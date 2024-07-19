using System;
using System.Collections.Generic;
using DoodleJump.Core;

namespace DoodleJump.Game.Worlds
{
    internal interface IPlatformStorage : IDestroyable
    {
        public float HighestPlatformY { get; }

        public IReadOnlyList<IPlatform> Platforms { get; }

        public event Action<IPlatform> Collided;

        public void TryGeneratePlatform();

        public void GeneratePlatforms();

        public void DestroyPlatform(IPlatform platform);
    }
}
