using System;
using DoodleJump.Core;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal interface IPlatform : IPoolable
    {
        public int Id { get; }

        public Vector2 Size { get; }

        public Vector3 Position { get; }

        public abstract event Action<IPlatformCollisionInfo> Collided;

        public abstract event Action<IPlatform> Destroyed;

        public void Init(Data.IGameData gameData);

        public void InitPosition(Vector3 position);

        public bool IsIntersectedArea(Vector2 center, Vector2 size);

        public void Destroy();
    }
}
