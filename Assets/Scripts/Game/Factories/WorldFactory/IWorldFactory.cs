using DoodleJump.Core.Factories;
using DoodleJump.Game.Worlds;

namespace DoodleJump.Game.Factories
{
    internal interface IWorldFactory : IFactory
    {
        public void Init(WorldFactoryArgs args);

        public IWorld CreateWorld(Worlds.Entities.IDoodler doodler);

        public Worlds.Platforms.IPlatform CreatePlatform<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Platforms.IPlatform;

        public Worlds.Entities.IEnemy CreateEnemy<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Entities.IEnemy;

        public Worlds.Boosters.IWorldBooster CreateBooster<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Boosters.IWorldBooster;
    }
}
