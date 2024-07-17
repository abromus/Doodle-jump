using DoodleJump.Core;
using DoodleJump.Game.Worlds.Entities;
using DoodleJump.Game.Factories;
using DoodleJump.Game.Worlds;

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
            CreateDoodler();
            CreateWorld();
        }

        public void Destroy()
        {
            DestroyWorld();
            DestroyDoodler();
        }

        private void CreateWorld()
        {
            var factory = _gameData.FactoryStorage.GetWorldFactory();

            _world = factory.CreateWorld(_doodler);
        }

        private void CreateDoodler()
        {
            var factory = _gameData.FactoryStorage.GetDoodlerFactory();

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
