namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerMovement
    {
        public void Jump(float height);

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);
    }
}
