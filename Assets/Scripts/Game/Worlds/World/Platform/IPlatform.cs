using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal interface IPlatform : IPoolable
    {
        public Vector2 Size{ get; }

        public Vector3 Position { get; }

        public void Init(Vector3 position);

        public bool IsIntersectedArea(Vector2 center, Vector2 size);
    }
}
