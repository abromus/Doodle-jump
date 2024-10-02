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

        internal readonly IGameData GameData => _gameData;

        internal readonly IUpdater Updater => _updater;

        internal readonly ICameraService CameraService => _cameraService;

        internal readonly IEventSystemService EventSystemService => _eventSystemService;

        internal readonly IScreenSystemService ScreenSystemService => _screenSystemService;

        internal readonly IAudioService AudioService => _audioService;

        internal readonly IPlatformTriggerFactory PlatformTriggerFactory => _platformTriggerFactory;

        internal readonly IEnemyTriggerFactory EnemyTriggerFactory => _enemyTriggerFactory;

        internal readonly IBoosterTriggerFactory BoosterTriggerFactory => _boosterTriggerFactory;

        internal readonly ICameraConfig CameraConfig => _cameraConfig;

        internal readonly IGeneratorConfig GeneratorConfig => _generatorConfig;

        internal readonly IPersistentDataStorage PersistentDataStorage => _persistentDataStorage;

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
