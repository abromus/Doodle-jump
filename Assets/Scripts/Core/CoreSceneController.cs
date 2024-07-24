using DoodleJump.Core.Data;
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
        private ICoreData _coreData;

        internal void CreateCoreData()
        {
            _updater = new Updater();
            _coreData = new CoreData(_configStorage, _updater, _uiServicesContainer);

            EnterInitState();
        }

        internal void Destroy()
        {
            _coreData.Destroy();
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

        private void LateUpdate()
        {
            _updater.LateTick(Time.deltaTime);
        }

        private void OnApplicationFocus(bool focus)
        {
            _updater.SetPause(focus == false);
        }

        private void OnApplicationPause(bool pause)
        {
            _updater.SetPause(pause);
        }

        private void OnDestroy()
        {
            Destroy();
        }

        private void EnterInitState()
        {
            _coreData.ServiceStorage.GetStateMachine().Enter<BootstrapState>();
        }
    }
}
