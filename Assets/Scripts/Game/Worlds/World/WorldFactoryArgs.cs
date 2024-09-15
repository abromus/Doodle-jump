using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Worlds
{
    internal readonly struct WorldFactoryArgs
    {
        private readonly IGameData _gameData;
        private readonly IUpdater _updater;
        private readonly ICameraService _cameraService;
        private readonly IEventSystemService _eventSystemService;
        private readonly IScreenSystemService _screenSystemService;
        private readonly IAudioService _audioService;
        private readonly IPlatformTriggerFactory _platformTriggerFactory;
        private readonly IEnemyTriggerFactory _enemyTriggerFactory;
        private readonly IBoosterTriggerFactory _boosterTriggerFactory;
        private readonly ICameraConfig _cameraConfig;
        private readonly IGeneratorConfig _generatorConfig;
        private readonly IPersistentDataStorage _persistentDataStorage;

        internal IGameData GameData => _gameData;

        internal IUpdater Updater => _updater;

        internal ICameraService CameraService => _cameraService;

        internal IEventSystemService EventSystemService => _eventSystemService;

        internal IScreenSystemService ScreenSystemService => _screenSystemService;

        internal IAudioService AudioService => _audioService;

        internal IPlatformTriggerFactory PlatformTriggerFactory => _platformTriggerFactory;

        internal IEnemyTriggerFactory EnemyTriggerFactory => _enemyTriggerFactory;

        internal IBoosterTriggerFactory BoosterTriggerFactory => _boosterTriggerFactory;

        internal ICameraConfig CameraConfig => _cameraConfig;

        internal IGeneratorConfig GeneratorConfig => _generatorConfig;

        internal IPersistentDataStorage PersistentDataStorage => _persistentDataStorage;

        internal WorldFactoryArgs(
            IGameData gameData,
            IUpdater updater,
            ICameraService cameraService,
            IEventSystemService eventSystemService,
            IScreenSystemService screenSystemService,
            IAudioService audioService,
            IPlatformTriggerFactory platformTriggerFactory,
            IEnemyTriggerFactory enemyTriggerFactory,
            IBoosterTriggerFactory boosterTriggerFactory,
            ICameraConfig cameraConfig,
            IGeneratorConfig generatorConfig,
            IPersistentDataStorage persistentDataStorage)
        {
            _gameData = gameData;
            _updater = updater;
            _cameraService = cameraService;
            _eventSystemService = eventSystemService;
            _screenSystemService = screenSystemService;
            _audioService = audioService;
            _platformTriggerFactory = platformTriggerFactory;
            _enemyTriggerFactory = enemyTriggerFactory;
            _boosterTriggerFactory = boosterTriggerFactory;
            _cameraConfig = cameraConfig;
            _generatorConfig = generatorConfig;
            _persistentDataStorage = persistentDataStorage;
        }
    }
}
