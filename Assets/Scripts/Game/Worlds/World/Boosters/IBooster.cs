using System;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Boosters
{
    internal interface IBooster : Core.IDestroyable, Core.Services.IUpdatable, Core.Services.IPausable, Core.IPoolable
    {
        public int Id { get; }

        public GameObject GameObject { get; }

        public Vector3 Position { get; }

        public abstract event Action<IBoosterCollisionInfo> Collided;

        public abstract event Action<IBooster> Destroyed;

        public void Init(Data.IGameData gameData);

        public void InitPosition(Vector3 position);
    }
}
