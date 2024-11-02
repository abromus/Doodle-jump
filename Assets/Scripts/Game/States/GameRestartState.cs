using DoodleJump.Game.Services;

namespace DoodleJump.Game.States
{
    internal sealed class GameRestartState : Core.Services.IEnterState<GameStateArgs>
    {
        private readonly Data.IGameData _gameData;
        private readonly Core.Services.IStateMachine _stateMachine;

        internal GameRestartState(Data.IGameData gameData, Core.Services.IStateMachine stateMachine)
        {
            _gameData = gameData;
            _stateMachine = stateMachine;
        }

        public void Enter(GameStateArgs args)
        {
            var screenSystemService = _gameData.ServiceStorage.GetScreenSystemService();
            screenSystemService.ShowScreen(UI.ScreenType.Game);

            args.World.Restart();

            _stateMachine.Enter<GameLoopState, GameStateArgs>(args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
