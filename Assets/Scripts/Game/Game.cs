using DoodleJump.Core;

namespace DoodleJump.Game
{
    internal sealed class Game : IGame
    {
        private readonly IGameData _gameData;

        public IGameData GameData => _gameData;

        internal Game(IGameData gameData)
        {
            _gameData = gameData;
        }

        public void Run()
        {
            CreateWorld();
        }

        public void Destroy()
        {
        }

        private void CreateWorld()
        {
        }
    }
}
