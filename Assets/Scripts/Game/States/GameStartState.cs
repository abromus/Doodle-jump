using DoodleJump.Game.Services;

namespace DoodleJump.Game.States
{
    internal sealed class GameStartState : Core.Services.IEnterState<GameStateArgs>
    {
        private GameStateArgs _args;
        private Worlds.IWorldData _worldData;

        private readonly Core.Services.IStateMachine _stateMachine;
        private readonly IScreenSystemService _screenSystemService;

        internal GameStartState(Data.IGameData gameData, Core.Services.IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _screenSystemService = gameData.ServiceStorage.GetScreenSystemService();
        }

        public void Enter(GameStateArgs args)
        {
            _args = args;
            _worldData = _args.World.WorldData;

            PrepareGame();
            ShowMenuScreen();
            Subscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit()
        {
            HideMenuScreen();
            Unsubscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void PrepareGame()
        {
            _args.World.Prepare();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void RestartGame()
        {
            _stateMachine.Enter<GameRestartState, GameStateArgs>(_args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void ShowMenuScreen()
        {
            _screenSystemService.ShowScreen(UI.ScreenType.Menu);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void HideMenuScreen()
        {
            _screenSystemService.HideScreen(UI.ScreenType.Menu);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _worldData.GameStarted += OnGameStarted;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            _worldData.GameStarted -= OnGameStarted;
        }

        private void OnGameStarted()
        {
            RestartGame();
        }
    }
}
