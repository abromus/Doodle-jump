namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodler : IEntity, Core.Services.IUpdatable
    {
        public UnityEngine.GameObject GameObject { get; }

        public UnityEngine.Vector2 Size { get; }

        public event System.Action Jumped;

        public void Init(in DoodlerArgs args);

        public void Jump(float height);

        public void AddBooster(Worlds.Boosters.IBoosterCollisionInfo info);

        public bool HasBooster(Worlds.Boosters.BoosterType boosterType);

        public void SetProjectileContainer(UnityEngine.Transform projectilesContainer);

        public void GameOver(GameOverType type);

        public void Restart();
    }
}
