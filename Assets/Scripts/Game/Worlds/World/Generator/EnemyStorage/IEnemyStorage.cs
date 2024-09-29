using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds
{
    internal interface IEnemyStorage : IDestroyable
    {
        public IReadOnlyList<IEnemy> Enemies { get; }

        public event System.Action<IProgressInfo, IEnemyCollisionInfo> Collided;

        public event System.Action<Boosters.IWorldBooster, Boosters.BoosterTriggerType> BoosterDropped;

        public void Clear();

        public void GenerateEnemies();

        public void DestroyEnemy(IEnemy enemy);
    }
}
