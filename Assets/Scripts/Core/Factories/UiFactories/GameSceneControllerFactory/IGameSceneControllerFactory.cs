namespace DoodleJump.Core.Factories
{
    internal interface IGameSceneControllerFactory : IFactory
    {
        public SceneController Create();
    }
}
