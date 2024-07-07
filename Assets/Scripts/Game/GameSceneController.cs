using DoodleJump.Core;
using DoodleJump.Game.Initializers;
using DoodleJump.Game.Settings;
using UnityEngine;

namespace DoodleJump.Game
{
    internal sealed class GameSceneController : SceneController
    {
        [SerializeField] private ConfigData _configData;

        private IGame _game;

        private IConfigInitializer _configInitializer;
        private IServiceInitializer _serviceInitializer;
        private IFactoryInitializer _factoryInitializer;

        public override void Run(IGameData gameData)
        {
            _game = new Game(gameData);

            InitializeConfigs();

            InitializeServices();

            InitializeFactories();
            CreateMST();
            _game.Run();
        }

        public override void Destroy()
        {
            _game.Destroy();
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void InitializeConfigs()
        {
            _configInitializer = new ConfigInitializer(_game, _configData);
            _configInitializer.Initialize();
        }

        private void InitializeServices()
        {
            _serviceInitializer = new ServiceInitializer(_game);
            _serviceInitializer.Initialize();
        }

        private void InitializeFactories()
        {
            _factoryInitializer = new FactoryInitializer(_game);
            _factoryInitializer.Initialize();
        }

        private void CreateMST()
        {
        }
    }
}
