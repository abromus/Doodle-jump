using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodler : IEntity, IFixedUpdatable
    {
        public GameObject GameObject { get; }

        public Vector2 Size { get; }

        public event System.Action Jumped;

        public void Init(in DoodlerArgs args);

        public void Jump(float height);

        public void AddBooster(Worlds.Boosters.IBoosterCollisionInfo info);

        public bool HasBooster(Worlds.Boosters.BoosterType boosterType);

        public void SetProjectileContainer(Transform projectilesContainer);

        public void Prepare();

        public void GameOver(GameOverType type);

        public void Restart();
    }
}
