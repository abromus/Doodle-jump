using DoodleJump.Core.Factories;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Factories
{
    internal interface IWorldFactory : IFactory
    {
        public void Init(WorldFactoryArgs args);

        public IWorld CreateWorld(IDoodler doodler);

        public IPlatform CreatePlatform(Platform prefab, Transform container);
    }
}
