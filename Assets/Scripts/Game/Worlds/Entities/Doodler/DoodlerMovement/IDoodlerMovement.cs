namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerMovement
    {
        public UnityEngine.Vector2 Velocity { get; }

        public event System.Action Jumped;

        public void Jump(float height);

        public void GameOver(GameOverType type);

        public void Restart();

        public void Tick(float deltaTime);

        public void FixedTick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
