using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Factories
{
    internal interface IWorldFactory : IFactory
    {
        public void Init(IUpdater updater, ICameraService cameraService, IGeneratorConfig generatorConfig);

        public IWorld CreateWorld(IDoodler doodler);

        public IPlatform CreatePlatform(Platform platformPrefab);
    }
}
