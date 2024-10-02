using UnityEngine;

namespace DoodleJump.Core.Factories
{
    internal sealed class GameSceneControllerFactory : BaseUiFactory, IGameSceneControllerFactory
    {
        [SerializeField] private BaseSceneController _gameSceneControllerPrefab;

        public override UiFactoryType UiFactoryType => UiFactoryType.GameSceneControllerFactory;

        public BaseSceneController Create()
        {
            var gameController = Instantiate(_gameSceneControllerPrefab);
            gameController.gameObject.RemoveCloneSuffix();

            return gameController;
        }
    }
}
