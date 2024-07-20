namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerVelocity
    {
        public UnityEngine.Vector2 Velocity { get; }

        public void Tick();

        public void FixedTick(float deltaTime);
    }
}
