using DoodleJump.Core;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Worlds;
using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game
{
    internal sealed class Game : IGame
    {
        private IWorld _world;
        private IDoodler _doodler;

        private readonly IGameData _gameData;

        public IGameData GameData => _gameData;

        internal Game(IGameData gameData)
        {
            _gameData = gameData;
        }

        public void Run()
        {
            var factoryStorage = _gameData.FactoryStorage;

            CreateDoodler(factoryStorage);
            CreateWorld(factoryStorage);
        }

        public void Destroy()
        {
            DestroyWorld();
            DestroyDoodler();
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
    }
}
