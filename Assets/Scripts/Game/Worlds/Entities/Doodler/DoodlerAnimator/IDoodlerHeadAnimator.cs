namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerHeadAnimator
    {
        public void Restart();

        public void Tick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
