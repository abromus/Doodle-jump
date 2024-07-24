using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerInput
    {
        public Vector2 Direction { get; }

        public void Tick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
