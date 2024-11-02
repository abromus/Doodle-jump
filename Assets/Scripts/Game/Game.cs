namespace DoodleJump.Game
{
    internal sealed class Game : IGame
    {
        private States.GameStateMachineArgs _args;
        private Core.Services.IStateMachine _gameStateMachine;

        private readonly Data.IGameData _gameData;

        public Data.IGameData GameData => _gameData;

        internal Game(Core.Data.ICoreData coreData, Settings.ConfigStorage configStorage, UnityEngine.Transform uiServicesContainer)
        {
            _gameData = new Data.GameData(coreData, configStorage, uiServicesContainer);
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

        private void InitGameStateMachine(Data.IGameData gameData)
        {
            _args = new();
            _gameStateMachine = new Core.Services.StateMachine();

            _gameStateMachine.Add(new States.GameInitializationState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameStartState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameRestartState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameLoopState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameOverState(gameData, _gameStateMachine));
            _gameStateMachine.Add(new States.GameDestructionState(gameData));
        }
    }
}
