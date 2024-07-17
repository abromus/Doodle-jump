using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal interface IPlatform : IPoolable
    {
        public Vector3 Position { get; }

        public void Init(Vector3 position);
    }
}
