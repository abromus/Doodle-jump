namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerShooting
    {
        public void SetProjectileContainer(UnityEngine.Transform projectilesContainer);

        public void Tick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
