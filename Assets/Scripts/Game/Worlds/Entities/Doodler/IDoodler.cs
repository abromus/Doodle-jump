using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodler : IEntity, IFixedUpdatable, ILateUpdatable
    {
        public GameObject GameObject { get; }

        public Vector2 Size { get; }

        public void Init(DoodlerArgs args);

        public void Jump(float height);

        public void SetProjectileContainer(Transform projectilesContainer);

        public void Restart();
    }
}
