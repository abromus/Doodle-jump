namespace DoodleJump.Core.Factories
{
    internal interface IGameSceneControllerFactory : IFactory
    {
        public BaseSceneController Create();
    }
}
