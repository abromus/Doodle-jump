using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds
{
    internal readonly struct WorldArgs
    {
        private readonly IUpdater _updater;
        private readonly ICameraService _cameraService;
        private readonly IEventSystemService _eventSystemService;
        private readonly IScreenSystemService _screenSystemService;
        private readonly IAudioService _audioService;
        private readonly IWorldFactory _worldFactory;
        private readonly ITriggerFactory _triggerFactory;
        private readonly IDoodler _doodler;
        private readonly ICameraConfig _cameraConfig;
        private readonly IGeneratorConfig _generatorConfig;
        private readonly IPlatformsConfig _platformsConfig;
        private readonly IPersistentDataStorage _persistentDataStorage;

        internal IUpdater Updater => _updater;

        internal ICameraService CameraService => _cameraService;

        internal IEventSystemService EventSystemService => _eventSystemService;

        internal IScreenSystemService ScreenSystemService => _screenSystemService;

        internal IAudioService AudioService => _audioService;

        internal IWorldFactory WorldFactory => _worldFactory;

        internal ITriggerFactory TriggerFactory => _triggerFactory;

        internal IDoodler Doodler => _doodler;

        internal ICameraConfig CameraConfig => _cameraConfig;

        internal IGeneratorConfig GeneratorConfig => _generatorConfig;

        internal IPlatformsConfig PlatformsConfig => _platformsConfig;

        internal IPersistentDataStorage PersistentDataStorage => _persistentDataStorage;

        internal WorldArgs(
            IUpdater updater,
            ICameraService cameraService,
            IEventSystemService eventSystemService,
            IScreenSystemService screenSystemService,
            IAudioService audioService,
            IWorldFactory worldFactory,
            ITriggerFactory triggerFactory,
            IDoodler doodler,
            ICameraConfig cameraConfig,
            IGeneratorConfig generatorConfig,
            IPlatformsConfig platformsConfig,
            IPersistentDataStorage persistentDataStorage)
        {
            _updater = updater;
            _cameraService = cameraService;
            _eventSystemService = eventSystemService;
            _screenSystemService = screenSystemService;
            _audioService = audioService;
            _worldFactory = worldFactory;
            _triggerFactory = triggerFactory;
            _doodler = doodler;
            _cameraConfig = cameraConfig;
            _generatorConfig = generatorConfig;
            _platformsConfig = platformsConfig;
            _persistentDataStorage = persistentDataStorage;
        }
    }
}
