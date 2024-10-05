using DoodleJump.Game.Services;

namespace DoodleJump.Game.States
{
    internal sealed class GameDestructionState : Core.Services.IEnterState<GameStateMachineArgs>
    {
        private readonly Data.IGameData _gameData;

        internal GameDestructionState(Data.IGameData gameData)
        {
            _gameData = gameData;
        }

        public void Enter(GameStateMachineArgs args)
        {
            var screenSystemService = _gameData.ServiceStorage.GetScreenSystemService();
            screenSystemService?.HideScreen(UI.ScreenType.Main);

            args.World?.Destroy();
            args.Doodler?.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
