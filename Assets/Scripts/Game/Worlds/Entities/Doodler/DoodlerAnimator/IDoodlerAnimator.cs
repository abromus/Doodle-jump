namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerAnimator
    {
        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
