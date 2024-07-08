namespace DoodleJump.Game.Entities
{
    internal interface IDoodlerVelocity
    {
        public void Tick();

        public void FixedTick(float deltaTime);
    }
}
