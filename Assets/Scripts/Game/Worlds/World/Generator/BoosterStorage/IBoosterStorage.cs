using System;
using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Worlds
{
    internal interface IBoosterStorage : IDestroyable
    {
        public IReadOnlyList<IWorldBooster> WorldBoosters { get; }

        public event Action<IProgressInfo, IBoosterCollisionInfo> Collided;

        public void Clear();

        public void GenerateBoosters();

        public void DestroyBooster(IWorldBooster booster);
    }
}
