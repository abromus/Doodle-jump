using DoodleJump.Core;

namespace DoodleJump.Game.Factories
{
    internal sealed class WorldFactory : Core.Factories.UiFactory, IWorldFactory
    {
        [UnityEngine.SerializeField] private Worlds.World _world;

        private Data.IGameData _gameData;
        private Core.Services.IUpdater _updater;
        private Core.Services.ICameraService _cameraService;
        private Core.Services.IEventSystemService _eventSystemService;
        private Services.IScreenSystemService _screenSystemService;
        private Services.IAudioService _audioService;
        private IPlatformTriggerFactory _platformTriggerFactory;
        private IEnemyTriggerFactory _enemyTriggerFactory;
        private IBoosterTriggerFactory _boosterTriggerFactory;
        private Settings.ICameraConfig _cameraConfig;
        private Settings.IGeneratorConfig _generatorConfig;
        private Data.IPersistentDataStorage _persistentDataStorage;

        public override Core.Factories.UiFactoryType UiFactoryType => Core.Factories.UiFactoryType.WorldFactory;

        public void Init(Worlds.WorldFactoryArgs args)
        {
            _gameData = args.GameData;
            _updater = args.Updater;
            _cameraService = args.CameraService;
            _eventSystemService = args.EventSystemService;
            _screenSystemService = args.ScreenSystemService;
            _audioService = args.AudioService;
            _platformTriggerFactory = args.PlatformTriggerFactory;
            _enemyTriggerFactory = args.EnemyTriggerFactory;
            _boosterTriggerFactory = args.BoosterTriggerFactory;
            _cameraConfig = args.CameraConfig;
            _generatorConfig = args.GeneratorConfig;
            _persistentDataStorage = args.PersistentDataStorage;
        }

        public Worlds.IWorld CreateWorld(Worlds.Entities.IDoodler doodler)
        {
            var args = new Worlds.WorldArgs(
                _updater,
                _cameraService,
                _eventSystemService,
                _screenSystemService,
                _audioService,
                this,
                _platformTriggerFactory,
                _enemyTriggerFactory,
                _boosterTriggerFactory,
                doodler,
                _cameraConfig,
                _generatorConfig,
                _persistentDataStorage);
            var world = Instantiate(_world);
            world.Init(_gameData, args);
            world.gameObject.RemoveCloneSuffix();

            return world;
        }

        public Worlds.Platforms.IPlatform CreatePlatform<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Platforms.IPlatform
        {
            var platform = Instantiate(prefab, container);
            platform.gameObject.RemoveCloneSuffix();

            return platform;
        }

        public Worlds.Entities.IEnemy CreateEnemy<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Entities.IEnemy
        {
            var enemy = Instantiate(prefab, container);
            enemy.gameObject.RemoveCloneSuffix();

            return enemy;
        }

        public Worlds.Boosters.IWorldBooster CreateBooster<T>(T prefab, UnityEngine.Transform container) where T : UnityEngine.MonoBehaviour, Worlds.Boosters.IWorldBooster
        {
            var booster = Instantiate(prefab, container);
            booster.gameObject.RemoveCloneSuffix();

            return booster;
        }
    }
}
