using System;
using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IEnemy : IEntity, IPoolable
    {
        public int Id { get; }

        public Vector2 Size { get; }

        public Vector3 Position { get; }

        public abstract event Action<IEnemyCollisionInfo> Collided;

        public event Action<Worlds.Boosters.IWorldBooster, Worlds.Boosters.BoosterTriggerType> BoosterDropped;

        public abstract event Action<IEnemy> Destroyed;

        public void Init(Data.IGameData gameData, Factories.IBoosterTriggerFactory boosterTriggerFactory);

        public void InitPosition(Vector3 position);

        public bool IsIntersectedArea(Vector2 center, Vector2 size);
    }
}
