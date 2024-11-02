namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IEnemy : IEntity, Core.IPoolable
    {
        public int Id { get; }

        public UnityEngine.Vector2 Size { get; }

        public UnityEngine.Vector3 Position { get; }

        public abstract event System.Action<IEnemyCollisionInfo> Collided;

        public event System.Action<Worlds.Boosters.IWorldBooster, Worlds.Boosters.BoosterTriggerType> BoosterDropped;

        public abstract event System.Action<IEnemy> Destroyed;

        public void Init(Data.IGameData gameData, Factories.IBoosterTriggerFactory boosterTriggerFactory);

        public void InitPosition(UnityEngine.Vector3 position);

        public bool IsIntersectedArea(UnityEngine.Vector2 center, UnityEngine.Vector2 size);
    }
}
