using DoodleJump.Core.Services;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Worlds
{
    internal readonly struct WorldFactoryArgs
    {
        private readonly IUpdater _updater;
        private readonly ICameraService _cameraService;
        private readonly IEventSystemService _eventSystemService;
        private readonly IScreenSystemService _screenSystemService;
        private readonly ITriggerFactory _triggerFactory;
        private readonly ICameraConfig _cameraConfig;
        private readonly IGeneratorConfig _generatorConfig;
        private readonly IPlatformsConfig _platformsConfig;

        internal IUpdater Updater => _updater;

        internal ICameraService CameraService => _cameraService;

        internal IEventSystemService EventSystemService => _eventSystemService;

        internal IScreenSystemService ScreenSystemService => _screenSystemService;

        internal ITriggerFactory TriggerFactory => _triggerFactory;

        internal ICameraConfig CameraConfig => _cameraConfig;

        internal IGeneratorConfig GeneratorConfig => _generatorConfig;

        internal IPlatformsConfig PlatformsConfig => _platformsConfig;

        internal WorldFactoryArgs(
            IUpdater updater,
            ICameraService cameraService,
            IEventSystemService eventSystemService,
            IScreenSystemService screenSystemService,
            ITriggerFactory triggerFactory,
            ICameraConfig cameraConfig,
            IGeneratorConfig generatorConfig,
            IPlatformsConfig platformsConfig)
        {
            _updater = updater;
            _cameraService = cameraService;
            _eventSystemService = eventSystemService;
            _screenSystemService = screenSystemService;
            _triggerFactory = triggerFactory;
            _cameraConfig = cameraConfig;
            _generatorConfig = generatorConfig;
            _platformsConfig = platformsConfig;
        }
    }
}
