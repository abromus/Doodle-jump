using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodler : IEntity
    {
        public Vector2 Size { get; }

        public void Init(DoodlerArgs args);

        public void Jump(float height);

        public void Restart();
    }
}
