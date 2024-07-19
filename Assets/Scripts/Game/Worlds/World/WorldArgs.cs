using DoodleJump.Core.Services;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds
{
    internal readonly struct WorldArgs
    {
        private readonly IUpdater _updater;
        private readonly ICameraService _cameraService;
        private readonly IWorldFactory _worldFactory;
        private readonly ITriggerFactory _triggerFactory;
        private readonly IDoodler _doodler;
        private readonly IGeneratorConfig _generatorConfig;
        private readonly IPlatformsConfig _platformsConfig;

        internal IUpdater Updater => _updater;

        internal ICameraService CameraService => _cameraService;

        internal IWorldFactory WorldFactory => _worldFactory;

        internal ITriggerFactory TriggerFactory => _triggerFactory;

        internal IDoodler Doodler => _doodler;

        internal IGeneratorConfig GeneratorConfig => _generatorConfig;

        internal IPlatformsConfig PlatformsConfig => _platformsConfig;

        internal WorldArgs(
            IUpdater updater,
            ICameraService cameraService,
            IWorldFactory worldFactory,
            ITriggerFactory triggerFactory,
            IDoodler doodler,
            IGeneratorConfig generatorConfig,
            IPlatformsConfig platformsConfig)
        {
            _updater = updater;
            _cameraService = cameraService;
            _worldFactory = worldFactory;
            _triggerFactory = triggerFactory;
            _doodler = doodler;
            _generatorConfig = generatorConfig;
            _platformsConfig = platformsConfig;
        }
    }
}
