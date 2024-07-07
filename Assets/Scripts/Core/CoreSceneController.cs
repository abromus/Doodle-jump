using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using DoodleJump.Core.States;
using UnityEngine;

namespace DoodleJump.Core
{
    internal sealed class CoreSceneController : MonoBehaviour
    {
        [SerializeField] private ConfigStorage _configStorage;
        [SerializeField] private Transform _uiServicesContainer;

        private IUpdater _updater;
        private IGameData _gameData;

        internal void CreateGameData()
        {
            _updater = new Updater();
            _gameData = new GameData(_configStorage, _updater, _uiServicesContainer);

            EnterInitState();
        }

        internal void Destroy()
        {
            _gameData.Destroy();
        }

        private void Awake()
        {
            _configStorage.Init();
        }

        private void Update()
        {
            _updater.Tick(Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _updater.FixedTick(Time.fixedDeltaTime);
        }

        private void EnterInitState()
        {
            _gameData.ServiceStorage.GetStateMachine().Enter<BootstrapState>();
        }

        private void OnDestroy()
        {
            Destroy();
        }
    }
}
