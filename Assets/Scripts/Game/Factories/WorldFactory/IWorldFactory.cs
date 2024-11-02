namespace DoodleJump.Game.Factories
{
    internal interface IWorldFactory : Core.Factories.IFactory
    {
        public void Init(in Worlds.WorldFactoryArgs args);

        public Worlds.IWorld CreateWorld(Worlds.Entities.IDoodler doodler);

        public Worlds.Platforms.IPlatform CreatePlatform<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Platforms.IPlatform;

        public Worlds.Entities.IEnemy CreateEnemy<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Entities.IEnemy;

        public Worlds.Boosters.IWorldBooster CreateBooster<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Boosters.IWorldBooster;
    }
}
