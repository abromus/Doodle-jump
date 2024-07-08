using System.Collections.Generic;
using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Services;
using DoodleJump.Game.Settings;

namespace DoodleJump.Game.Initializers
{
    internal sealed class FactoryInitializer : IFactoryInitializer
    {
        private readonly IGame _game;
        private readonly IFactoryStorage _factoryStorage;

        internal FactoryInitializer(IGame game)
        {
            _game = game;

            _factoryStorage = _game.GameData.FactoryStorage;
        }

        public void Initialize()
        {
            InitFactories();
        }

        private void InitFactories()
        {
            var gameData = _game.GameData;
            var configStorage = gameData.ConfigStorage;
            var serviceStorage = gameData.ServiceStorage;
            var uiFactories = configStorage.GetUiFactoryConfig().UiFactories;

            InitDoodlerFactory(configStorage, serviceStorage, uiFactories);
        }

        private void InitDoodlerFactory(IConfigStorage configStorage, IServiceStorage serviceStorage, IReadOnlyList<IUiFactory> uiFactories)
        {
            var updater = serviceStorage.GetUpdater();
            var inputService = serviceStorage.GetInputService();
            var cameraService = serviceStorage.GetCameraService();
            var cameraConfig = configStorage.GetCameraConfig();
            var doodlerConfig = configStorage.GetDoodlerConfig();
            var args = new Entities.DoodlerArgs(
                updater,
                inputService,
                cameraService,
                cameraConfig,
                doodlerConfig);

            var factory = uiFactories.GetDoodlerFactory();
            factory.Init(args);

            _factoryStorage.AddFactory(factory);
        }
    }
}
