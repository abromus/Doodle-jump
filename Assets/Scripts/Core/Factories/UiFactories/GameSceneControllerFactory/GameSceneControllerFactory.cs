using UnityEngine;

namespace DoodleJump.Core.Factories
{
    internal sealed class GameSceneControllerFactory : UiFactory, IGameSceneControllerFactory
    {
        [SerializeField] private SceneController _gameSceneControllerPrefab;

        public override UiFactoryType UiFactoryType => UiFactoryType.GameSceneControllerFactory;

        public SceneController Create()
        {
            var gameController = Instantiate(_gameSceneControllerPrefab);
            gameController.gameObject.RemoveCloneSuffix();

            return gameController;
        }
    }
}
