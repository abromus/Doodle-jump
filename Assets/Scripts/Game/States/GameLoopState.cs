using DoodleJump.Game.Services;

namespace DoodleJump.Game.States
{
    internal sealed class GameLoopState : Core.Services.IEnterState<GameStateArgs>
    {
        private GameStateArgs _args;

        private readonly Core.Services.IStateMachine _stateMachine;
        private readonly IScreenSystemService _screenSystemService;

        internal GameLoopState(Data.IGameData gameData, Core.Services.IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _screenSystemService = gameData.ServiceStorage.GetScreenSystemService();
        }

        public void Enter(GameStateArgs args)
        {
            _args = args;

            _args.World.WorldData.GameOvered += OnGameOvered;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit()
        {
            _args.World.WorldData.GameOvered -= OnGameOvered;
        }

        private void OnGameOvered(Worlds.GameOverType type)
        {
            _screenSystemService.HideScreen(UI.ScreenType.Game);

            _stateMachine.Enter<GameOverState, GameStateArgs, Worlds.GameOverType>(_args, type);
        }
    }
}
