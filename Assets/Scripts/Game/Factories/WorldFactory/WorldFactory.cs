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
        private ITriggerFactory _triggerFactory;
        private IGeneratorConfig _generatorConfig;
        private IPlatformsConfig _platformsConfig;

        public override UiFactoryType UiFactoryType => UiFactoryType.WorldFactory;

        public void Init(IUpdater updater, ICameraService cameraService, ITriggerFactory triggerFactory, IGeneratorConfig generatorConfig, IPlatformsConfig platformsConfig)
        {
            _updater = updater;
            _cameraService = cameraService;
            _triggerFactory = triggerFactory;
            _generatorConfig = generatorConfig;
            _platformsConfig = platformsConfig;
        }

        public IWorld CreateWorld(IDoodler doodler)
        {
            var args = new WorldArgs(_updater, _cameraService, this, _triggerFactory, doodler, _generatorConfig, _platformsConfig);
            var world = Instantiate(_world);
            world.Init(args);
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
