using System;
using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds
{
    internal interface IEnemyStorage : IDestroyable
    {
        public IReadOnlyList<IEnemy> Enemies { get; }

        public event Action<IProgressInfo, IEnemyCollisionInfo> Collided;

        public void Clear();

        public void GenerateEnemies();

        public void DestroyEnemy(IEnemy enemy);
    }
}
