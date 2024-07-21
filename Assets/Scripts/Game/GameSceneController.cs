using DoodleJump.Core;
using DoodleJump.Core.Data;
using DoodleJump.Game.Settings;
using UnityEngine;

namespace DoodleJump.Game
{
    internal sealed class GameSceneController : SceneController
    {
        [SerializeField] private ConfigStorage _configStorage;
        [SerializeField] private Transform _uiServicesContainer;

        private IGame _game;

        public override void Run(ICoreData coreData)
        {
            _game = new Game(coreData, _configStorage, _uiServicesContainer);
            _game.Run();
        }

        public override void Destroy()
        {
            _game.Destroy();
        }

        private void Awake()
        {
            _configStorage.Init();
        }

        private void OnDestroy()
        {
            Destroy();
        }
    }
}
