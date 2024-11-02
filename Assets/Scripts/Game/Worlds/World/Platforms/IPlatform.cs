namespace DoodleJump.Game.Worlds.Platforms
{
    internal interface IPlatform : Core.IPoolable
    {
        public int Id { get; }

        public UnityEngine.Vector2 Size { get; }

        public UnityEngine.Vector3 Position { get; }

        public abstract event System.Action<IPlatformCollisionInfo> Collided;

        public abstract event System.Action<IPlatform> Destroyed;

        public void Init(Data.IGameData gameData);

        public void InitBooster(Boosters.IWorldBooster booster);

        public void InitConfig(Settings.IPlatformConfig platformConfig);

        public void InitPosition(UnityEngine.Vector3 position);

        public bool IsIntersectedArea(UnityEngine.Vector2 center, UnityEngine.Vector2 size);

        public void Destroy();
    }
}
