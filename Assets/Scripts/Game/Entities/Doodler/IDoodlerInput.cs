using UnityEngine;

namespace DoodleJump.Game.Entities
{
    public interface IDoodlerInput
    {
        public Vector2 Direction { get; }

        public void Tick(float deltaTime);
    }
}
