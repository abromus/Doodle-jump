using DoodleJump.Core.Services;

namespace DoodleJump.Core
{
    internal sealed class CoreSceneController : UnityEngine.MonoBehaviour
    {
        [UnityEngine.SerializeField] private Settings.ConfigStorage _configStorage;
        [UnityEngine.SerializeField] private UnityEngine.Transform _uiServicesContainer;

        private IUpdater _updater;
        private Data.ICoreData _coreData;

        internal void CreateCoreData()
        {
            _updater = new Updater();
            _coreData = new Data.CoreData(_configStorage, _updater, _uiServicesContainer);

            EnterInitState();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
            _updater.Tick(UnityEngine.Time.deltaTime);
        }

        private void FixedUpdate()
        {
            _updater.FixedTick(UnityEngine.Time.fixedDeltaTime);
        }

        private void LateUpdate()
        {
            _updater.LateTick(UnityEngine.Time.deltaTime);
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

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void EnterInitState()
        {
            _coreData.ServiceStorage.GetStateMachine().Enter<States.BootstrapState>();
        }
    }
}
