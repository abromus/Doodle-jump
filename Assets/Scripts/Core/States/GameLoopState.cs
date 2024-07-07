using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;

namespace DoodleJump.Core.States
{
    internal sealed class GameLoopState : IEnterState
    {
        private readonly IGameData _gameData;

        internal GameLoopState(IGameData gameData)
        {
            _gameData = gameData;
        }

        public void Enter()
        {
            var gameSceneControllerFactory = _gameData.FactoryStorage.GetGameSceneControllerFactory();

            var gameSceneController = gameSceneControllerFactory.Create();
            gameSceneController.Run(_gameData);
        }

        public void Exit() { }
    }
}
