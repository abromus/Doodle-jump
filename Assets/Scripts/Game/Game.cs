using DoodleJump.Core.Data;
using DoodleJump.Game.Data;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Services;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game
{
    internal sealed class Game : IGame
    {
        private IWorld _world;
        private IDoodler _doodler;
        private IScreenSystemService _screenSystemService;

        private readonly IGameData _gameData;

        public IGameData GameData => _gameData;

        internal Game(ICoreData coreData, Settings.ConfigStorage configStorage, UnityEngine.Transform uiServicesContainer)
        {
            _gameData = new GameData(coreData, configStorage, uiServicesContainer);
        }

        public void Run()
        {
            var factoryStorage = _gameData.FactoryStorage;

            CreateDoodler(factoryStorage);
            CreateWorld(factoryStorage);
            ShowMainScreen();
        }

        public void Destroy()
        {
            HideMainScreen();
            DestroyWorld();
            DestroyDoodler();

            _gameData.Destroy();
        }

        private void CreateWorld(Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetWorldFactory();

            _world = factory.CreateWorld(_doodler);
        }

        private void CreateDoodler(Core.Factories.IFactoryStorage factoryStorage)
        {
            var factory = factoryStorage.GetDoodlerFactory();

            _doodler = factory.Create();
        }

        private void ShowMainScreen()
        {
            _screenSystemService = _gameData.ServiceStorage.GetScreenSystemService();
            _screenSystemService.ShowScreen(UI.ScreenType.Main);
        }

        private void DestroyWorld()
        {
            _world?.Destroy();
            _world = null;
        }

        private void DestroyDoodler()
        {
            _doodler?.Destroy();
            _doodler = null;
        }

        private void HideMainScreen()
        {
            _screenSystemService?.HideScreen(UI.ScreenType.Main);
            _screenSystemService = null;
        }
    }
}
