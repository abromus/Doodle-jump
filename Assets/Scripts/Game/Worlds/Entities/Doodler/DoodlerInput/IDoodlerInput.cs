namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerInput
    {
        public UnityEngine.Vector2 MoveDirection { get; }

        public bool IsShooting { get; }

        public UnityEngine.Vector2 ShootPosition { get; }

        public void Tick(float deltaTime);

        public void SetPause(bool isPaused);
    }
}
