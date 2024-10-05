using DoodleJump.Core.Data;
using DoodleJump.Game.Data;

namespace DoodleJump.Game
{
    internal sealed class Game : IGame
    {
        private States.GameStateMachineArgs _args;
        private Core.Services.IStateMachine _gameStateMachine;

        private readonly IGameData _gameData;

        public IGameData GameData => _gameData;

        internal Game(ICoreData coreData, Settings.ConfigStorage configStorage, UnityEngine.Transform uiServicesContainer)
        {
            _gameData = new GameData(coreData, configStorage, uiServicesContainer);
        }

        public void Run()
        {
            InitGameStateMachine(_gameData);

            _gameStateMachine.Enter<States.GameInitializationState, States.GameStateMachineArgs>(_args);
        }

        public void Destroy()
        {
            _gameStateMachine.Enter<States.GameDestructionState, States.GameStateMachineArgs>(_args);

            _gameData.Destroy();
        }

        private void InitGameStateMachine(IGameData gameData)
        {
            _args = new();
            _gameStateMachine = new Core.Services.StateMachine();

            _gameStateMachine.Add(new States.GameInitializationState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameRestartState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameLoopState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameOverState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameDestructionState(gameData));
        }
    }
}
