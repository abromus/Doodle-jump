using DoodleJump.Core.Data;
using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;

namespace DoodleJump.Core.States
{
    internal sealed class GameLoopState : IEnterState
    {
        private readonly ICoreData _coreData;

        internal GameLoopState(ICoreData coreData)
        {
            _coreData = coreData;
        }

        public void Enter()
        {
            var gameSceneControllerFactory = _coreData.FactoryStorage.GetGameSceneControllerFactory();

            var gameSceneController = gameSceneControllerFactory.Create();
            gameSceneController.Run(_coreData);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
