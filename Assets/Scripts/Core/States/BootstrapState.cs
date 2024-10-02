using DoodleJump.Core.Services;
using UnityEngine.SceneManagement;

namespace DoodleJump.Core.States
{
    internal sealed class BootstrapState : IEnterState
    {
        private readonly IStateMachine _stateMachine;

        internal BootstrapState(IStateMachine stateMachine)
        {
            _stateMachine = stateMachine;
        }

        public void Enter()
        {
            var isLoadAsync = false;
            var isActive = true;
            var coreSceneInfo = new SceneInfo(SceneKeys.CoreSceneName, LoadSceneMode.Single, isLoadAsync, isActive, OnSceneLoad);

            _stateMachine.Enter<SceneLoaderState, SceneInfo>(coreSceneInfo);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }

        private void OnSceneLoad()
        {
            _stateMachine.Enter<GameState>();
        }
    }
}
