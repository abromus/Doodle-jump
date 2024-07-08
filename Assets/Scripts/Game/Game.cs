using DoodleJump.Core;
using DoodleJump.Game.Entities;
using DoodleJump.Game.Factories;

namespace DoodleJump.Game
{
    internal sealed class Game : IGame
    {
        private IDoodler _doodler;

        private readonly IGameData _gameData;

        public IGameData GameData => _gameData;

        internal Game(IGameData gameData)
        {
            _gameData = gameData;
        }

        public void Run()
        {
            CreateWorld();
            CreateDoodler();
        }

        public void Destroy()
        {
            DestroyWorld();
            DestroyDoodler();
        }

        private void CreateWorld()
        {
        }

        private void CreateDoodler()
        {
            var factory = _gameData.FactoryStorage.GetDoodlerFactory();

            _doodler = factory.Create();
        }

        private void DestroyWorld()
        {
        }

        private void DestroyDoodler()
        {
            _doodler?.Destroy();
            _doodler = null;
        }
    }
}
