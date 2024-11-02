namespace DoodleJump.Core.Services
{
    internal interface ISceneLoader : IService
    {
        public void Load(in SceneInfo sceneInfo);
    }
}
