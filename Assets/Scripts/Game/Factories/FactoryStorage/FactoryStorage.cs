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

        private readonly IPersistentDataStorage _persistentDataStorage;
        private readonly IConfigStorage _configStorage;
        private readonly IServiceStorage _serviceStorage;
        private readonly IUiFactoryConfig _uiFactoryConfig;

        internal FactoryStorage(ICoreData coreData, IPersistentDataStorage persistentDataStorage, IConfigStorage configStorage, IServiceStorage serviceStorage)
        {
            _persistentDataStorage = persistentDataStorage;
            _configStorage = configStorage;
            _serviceStorage = serviceStorage;

            _uiFactoryConfig = configStorage.GetGameUiFactoryConfig();

            InitFactories(coreData);
        }

        public void AddFactory<TFactory>(TFactory factory) where TFactory : class, IFactory
        {
            var type = typeof(TFactory);

            if (_factories.ContainsKey(type))
                _factories[type] = factory;
            else
                _factories.Add(type, factory);
        }

        public TFactory GetFactory<TFactory>() where TFactory : class, IFactory
        {
            return _factories[typeof(TFactory)] as TFactory;
        }

        public void Destroy()
        {
            foreach (var factory in _factories.Values)
                factory.Destroy();

            _factories.Clear();
            _factories = null;
        }

        private void InitFactories(ICoreData coreData)
        {
            var uiFactories = _uiFactoryConfig.UiFactories;
            var coreServiceStorage = coreData.ServiceStorage;
            var updater = coreServiceStorage.GetUpdater();
            var cameraService = coreServiceStorage.GetCameraService();
            var doodlerFactory = InitDoodlerFactory(coreServiceStorage, updater, cameraService, uiFactories);
            var triggerFactory = InitTriggerFactory();
            var worldFactory = InitWorldFactory(coreServiceStorage, updater, cameraService, uiFactories, triggerFactory);

            _factories = new(8)
            {
                [typeof(IDoodlerFactory)] = doodlerFactory,
                [typeof(ITriggerFactory)] = triggerFactory,
                [typeof(IWorldFactory)] = worldFactory,
            };
        }

        private IDoodlerFactory InitDoodlerFactory(IServiceStorage coreServiceStorage, IUpdater updater, ICameraService cameraService, IReadOnlyList<IUiFactory> uiFactories)
        {
            var inputService = coreServiceStorage.GetInputService();
            var doodlerConfig = _configStorage.GetDoodlerConfig();
            var args = new Worlds.Entities.DoodlerArgs(
                updater,
                inputService,
                cameraService,
                doodlerConfig);

            var factory = uiFactories.GetDoodlerFactory();
            factory.Init(args);

            return factory;
        }

        private ITriggerFactory InitTriggerFactory()
        {
            var factory = new TriggerFactory();

            return factory;
        }

        private IWorldFactory InitWorldFactory(
            IServiceStorage coreServiceStorage,
            IUpdater updater,
            ICameraService cameraService,
            IReadOnlyList<IUiFactory> uiFactories,
            ITriggerFactory triggerFactory)
        {
            var eventSystemService = coreServiceStorage.GetEventSystemService();
            var screenSystemService = _serviceStorage.GetScreenSystemService();
            var audioService = _serviceStorage.GetAudioService();
            var factory = uiFactories.GetWorldFactory();
            var cameraConfig = _configStorage.GetCameraConfig();
            var generatorConfig = _configStorage.GetGeneratorConfig();
            var platformsConfig = _configStorage.GetPlatformsConfig();
            var args = new Worlds.WorldFactoryArgs(
                updater,
                cameraService,
                eventSystemService,
                screenSystemService,
                audioService,
                triggerFactory,
                cameraConfig,
                generatorConfig,
                platformsConfig,
                _persistentDataStorage);
            factory.Init(args);

            return factory;
        }
    }
}
