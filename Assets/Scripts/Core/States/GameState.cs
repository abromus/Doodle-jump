using DoodleJump.Core.Services;
using UnityEngine.SceneManagement;

namespace DoodleJump.Core.States
{
    internal sealed class GameState : IEnterState
    {
        private readonly IStateMachine _stateMachine;

        internal GameState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            var isLoadAsync = true;
            var isActive = true;

            var gameSceneInfo = new SceneInfo(SceneKeys.GameSceneName, LoadSceneMode.Additive, isLoadAsync, isActive, OnSceneLoad);

            _stateMachine.Enter<SceneLoaderState, SceneInfo>(gameSceneInfo);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }

        private void OnSceneLoad()
        {
            _stateMachine.Enter<GameLoopState>();
        }
    }
}
