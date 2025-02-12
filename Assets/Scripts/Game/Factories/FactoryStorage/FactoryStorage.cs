using System;
using System.Collections.Generic;
using DoodleJump.Core.Data;
using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Factories
{
    internal sealed class FactoryStorage : IFactoryStorage
    {
        private Dictionary<Type, IFactory> _factories;

        private readonly IConfigStorage _configStorage;
        private readonly IServiceStorage _serviceStorage;
        private readonly IUiFactoryConfig _uiFactoryConfig;

        internal FactoryStorage(IGameData gameData, ICoreData coreData, IConfigStorage configStorage, IServiceStorage serviceStorage)
        {
            _configStorage = configStorage;
            _serviceStorage = serviceStorage;

            _uiFactoryConfig = configStorage.GetGameUiFactoryConfig();

            InitFactories(gameData, coreData);
        }

        public void AddFactory<TFactory>(TFactory factory) where TFactory : class, IFactory
        {
            var type = typeof(TFactory);

            if (_factories.ContainsKey(type))
                _factories[type] = factory;
            else
                _factories.Add(type, factory);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public TFactory GetFactory<TFactory>() where TFactory : class, IFactory
        {
            return _factories[typeof(TFactory)] as TFactory;
        }

        public void Destroy()
        {
            _factories.Clear();
            _factories = null;
        }

        private void InitFactories(IGameData gameData, ICoreData coreData)
        {
            var uiFactories = _uiFactoryConfig.UiFactories;
            var gameServiceStorage = gameData.ServiceStorage;
            var coreServiceStorage = coreData.ServiceStorage;
            var updater = coreServiceStorage.GetUpdater();
            var cameraService = coreServiceStorage.GetCameraService();
            var persistentDataStorage = _serviceStorage.GetSaveLoadService().PersistentDataStorage;
            var boostersConfig = _configStorage.GetBoostersConfig();

            var boosterFactory = InitBoosterFactory();
            var doodlerFactory = InitDoodlerFactory(gameServiceStorage, coreServiceStorage, updater, cameraService, uiFactories, boosterFactory, boostersConfig, persistentDataStorage);
            var platformTriggerFactory = InitPlatformTriggerFactory();
            var enemyTriggerFactory = InitEnemyTriggerFactory();
            var boosterTriggerFactory = InitBoosterTriggerFactory();
            var worldFactory = InitWorldFactory(gameData, coreServiceStorage, updater, cameraService, uiFactories, platformTriggerFactory, enemyTriggerFactory, boosterTriggerFactory, persistentDataStorage);

            _factories = new(8)
            {
                [typeof(IDoodlerFactory)] = doodlerFactory,
                [typeof(IPlatformTriggerFactory)] = platformTriggerFactory,
                [typeof(IEnemyTriggerFactory)] = enemyTriggerFactory,
                [typeof(IWorldFactory)] = worldFactory,
                [typeof(IBoosterFactory)] = boosterFactory,
            };
        }

        private IDoodlerFactory InitDoodlerFactory(
            IServiceStorage gameServiceStorage,
            IServiceStorage coreServiceStorage,
            IUpdater updater,
            ICameraService cameraService,
            IReadOnlyList<IUiFactory> uiFactories,
            IBoosterFactory boosterFactory,
            IBoostersConfig boostersConfig,
            IPersistentDataStorage persistentDataStorage)
        {
            var audioService = gameServiceStorage.GetAudioService();
            var inputService = coreServiceStorage.GetInputService();
            var doodlerConfig = _configStorage.GetDoodlerConfig();
            var playerData = persistentDataStorage.GetPlayerData();
            var args = new Worlds.Entities.DoodlerArgs(
                updater,
                audioService,
                inputService,
                cameraService,
                boosterFactory,
                doodlerConfig,
                boostersConfig,
                playerData);

            var factory = uiFactories.GetDoodlerFactory();
            factory.Init(in args);

            return factory;
        }

        private IPlatformTriggerFactory InitPlatformTriggerFactory()
        {
            var factory = new PlatformTriggerFactory();

            return factory;
        }

        private IEnemyTriggerFactory InitEnemyTriggerFactory()
        {
            var factory = new EnemyTriggerFactory();

            return factory;
        }

        private IBoosterFactory InitBoosterFactory()
        {
            var factory = new BoosterFactory();

            return factory;
        }

        private IBoosterTriggerFactory InitBoosterTriggerFactory()
        {
            var factory = new BoosterTriggerFactory();

            return factory;
        }

        private IWorldFactory InitWorldFactory(
            IGameData gameData,
            IServiceStorage coreServiceStorage,
            IUpdater updater,
            ICameraService cameraService,
            IReadOnlyList<IUiFactory> uiFactories,
            IPlatformTriggerFactory platformTriggerFactory,
            IEnemyTriggerFactory enemyTriggerFactory,
            IBoosterTriggerFactory boosterTriggerFactory,
            IPersistentDataStorage persistentDataStorage)
        {
            var eventSystemService = coreServiceStorage.GetEventSystemService();
            var screenSystemService = _serviceStorage.GetScreenSystemService();
            var audioService = _serviceStorage.GetAudioService();
            var factory = uiFactories.GetWorldFactory();
            var cameraConfig = _configStorage.GetCameraConfig();
            var generatorConfig = _configStorage.GetGeneratorConfig();
            var args = new Worlds.WorldFactoryArgs(
                gameData,
                updater,
                cameraService,
                eventSystemService,
                screenSystemService,
                audioService,
                platformTriggerFactory,
                enemyTriggerFactory,
                boosterTriggerFactory,
                cameraConfig,
                generatorConfig,
                persistentDataStorage);
            factory.Init(in args);

            return factory;
        }
    }
}
