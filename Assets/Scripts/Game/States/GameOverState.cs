using DoodleJump.Core.Services;
using DoodleJump.Game.Services;

namespace DoodleJump.Game.States
{
    internal sealed class GameOverState : IEnterState<GameStateArgs, Worlds.GameOverType>
    {
        private GameStateArgs _args;
        private Worlds.IWorldData _worldData;

        private readonly IStateMachine _stateMachine;
        private readonly IScreenSystemService _screenSystemService;

        internal GameOverState(Data.IGameData gameData, IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
            _screenSystemService = gameData.ServiceStorage.GetScreenSystemService();
        }

        public void Enter(GameStateArgs args, Worlds.GameOverType type)
        {
            _args = args;
            _worldData = _args.World.WorldData;

            if (type != Worlds.GameOverType.User)
            {
                GameOver(type);
                ShowGameOverScreen(type);
                Subscribe();
            }
            else
            {
                RestartGame();
            }
        }

        public void Exit()
        {
            HideGameOverScreen();
            Unsubscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void GameOver(Worlds.GameOverType type)
        {
            _args.World.GameOver(type);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void RestartGame()
        {
            _stateMachine.Enter<GameRestartState, GameStateArgs>(_args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void ShowGameOverScreen(Worlds.GameOverType type)
        {
            var args = new UI.GameOverScreenArgs(type) as IScreenArgs;

            _screenSystemService.ShowScreen(UI.ScreenType.GameOver, args);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void HideGameOverScreen()
        {
            _screenSystemService?.HideScreen(UI.ScreenType.GameOver);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Subscribe()
        {
            _worldData.GameRestarted += OnGameRestarted;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Unsubscribe()
        {
            _worldData.GameRestarted -= OnGameRestarted;
        }

        private void OnGameRestarted()
        {
            RestartGame();
        }
    }
}
