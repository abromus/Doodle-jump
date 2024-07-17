using DoodleJump.Core;
using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Factories
{
    internal sealed class WorldFactory : UiFactory, IWorldFactory
    {
        [SerializeField] private World _world;

        private IUpdater _updater;
        private ICameraService _cameraService;
        private IGeneratorConfig _generatorConfig;

        public override UiFactoryType UiFactoryType => UiFactoryType.WorldFactory;

        public void Init(IUpdater updater, ICameraService cameraService, IGeneratorConfig generatorConfig)
        {
            _updater = updater;
            _cameraService = cameraService;
            _generatorConfig = generatorConfig;
        }

        public IWorld CreateWorld(IDoodler doodler)
        {
            var world = Instantiate(_world);
            world.Init(_updater, _cameraService, this, doodler, _generatorConfig);
            world.gameObject.RemoveCloneSuffix();

            return world;
        }

        public IPlatform CreatePlatform(Platform platformPrefab)
        {
            var platform = Instantiate(platformPrefab);
            platform.gameObject.RemoveCloneSuffix();

            return platform;
        }
    }
}
