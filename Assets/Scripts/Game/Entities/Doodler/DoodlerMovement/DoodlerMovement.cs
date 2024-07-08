namespace DoodleJump.Game.Entities
{
    internal sealed class DoodlerMovement : IDoodlerMovement
    {
        private readonly IDoodlerVelocity _velocity;
        private readonly IDoodlerJump _jump;

        public DoodlerMovement(in DoodlerMovementArgs args)
        {
            _velocity = new DoodlerVelocity(in args);
            _jump = new DoodlerJump(in args);
        }

        public void Jump(float height)
        {
            _jump.Do(height);
        }

        public void Tick(float deltaTime)
        {
            _velocity.Tick();
        }

        public void FixedTick(float deltaTime)
        {
            _velocity.FixedTick(deltaTime);
        }
    }
}
