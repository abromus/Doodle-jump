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

        public void Enter(SceneInfo sceneInfo)
        {
            _sceneLoader.Load(sceneInfo);
        }

        public void Exit() { }
    }
}
