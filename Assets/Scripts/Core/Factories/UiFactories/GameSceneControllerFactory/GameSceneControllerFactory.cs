namespace DoodleJump.Core.Factories
{
    internal sealed class GameSceneControllerFactory : BaseUiFactory, IGameSceneControllerFactory
    {
        [UnityEngine.SerializeField] private BaseSceneController _gameSceneControllerPrefab;

        public BaseSceneController Create()
        {
            var gameController = Instantiate(_gameSceneControllerPrefab);
            gameController.gameObject.RemoveCloneSuffix();

            return gameController;
        }
    }
}
