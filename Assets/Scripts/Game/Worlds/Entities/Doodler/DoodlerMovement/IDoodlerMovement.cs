namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerMovement
    {
        public UnityEngine.Vector2 Velocity { get; }

        public void Jump(float height);

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
