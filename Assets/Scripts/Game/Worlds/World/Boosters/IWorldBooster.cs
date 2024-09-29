using System;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Boosters
{
    internal interface IWorldBooster : Core.IDestroyable, Core.Services.IUpdatable, Core.Services.IPausable, Core.IPoolable
    {
        public int Id { get; }

        public GameObject GameObject { get; }

        public BoosterType BoosterType { get; }

        public Vector3 Position { get; }

        public abstract IBoosterCollisionInfo Info { get; }

        public abstract event Action<IBoosterCollisionInfo> Collided;

        public abstract event Action<IWorldBooster> Destroyed;

        public void Init(Data.IGameData gameData);

        public void InitPosition(Vector3 position);
    }
}
