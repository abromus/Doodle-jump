using DoodleJump.Core.Services;

namespace DoodleJump.Core.States
{
    internal sealed class SceneLoaderState : IEnterState<SceneInfo>
    {
        private readonly ISceneLoader _sceneLoader;

        internal SceneLoaderState(ISceneLoader sceneLoader)
        {
            _sceneLoader = sceneLoader;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Enter(SceneInfo sceneInfo)
        {
            _sceneLoader.Load(sceneInfo);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Exit() { }
    }
}
