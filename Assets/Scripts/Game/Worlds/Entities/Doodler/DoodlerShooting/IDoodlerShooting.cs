namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerShooting : Core.IDestroyable
    {
        public void SetProjectileContainer(UnityEngine.Transform projectilesContainer);

        public void Restart();

        public void Tick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
