using DoodleJump.Core.Factories;
using DoodleJump.Game.Worlds;
using UnityEngine;

namespace DoodleJump.Game.Factories
{
    internal interface IWorldFactory : IFactory
    {
        public void Init(WorldFactoryArgs args);

        public IWorld CreateWorld(Worlds.Entities.IDoodler doodler);

        public Worlds.Platforms.IPlatform CreatePlatform(Worlds.Platforms.Platform prefab, Transform container);

        public Worlds.Entities.IEnemy CreateEnemy(Worlds.Entities.Enemy prefab, Transform container);

        public Worlds.Boosters.IBooster CreateBooster(Worlds.Boosters.Booster prefab);
    }
}
