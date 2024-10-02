using DoodleJump.Core.Services;
using DoodleJump.Game.Data;

namespace DoodleJump.Game.Services
{
    internal sealed class SaveLoadService : ISaveLoadService, ILateUpdatable, IPausable
    {
        private readonly IUpdater _updater;
        private readonly IPersistentDataStorage _persistentDataStorage;

        public IPersistentDataStorage PersistentDataStorage => _persistentDataStorage;

        internal SaveLoadService(IUpdater updater, Settings.IDoodlerConfig doodlerConfig)
        {
            _updater = updater;

            _persistentDataStorage = new PersistentDataStorage(doodlerConfig);
            _persistentDataStorage.Init();

            _updater.AddLateUpdatable(this);
            _updater.AddPausable(this);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void LateTick(float deltaTime)
        {
            Save();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            Save();
        }

        public void Destroy()
        {
            Save();

            _persistentDataStorage.Dispose();

            _updater.RemoveLateUpdatable(this);
            _updater.RemovePausable(this);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void Save()
        {
            _persistentDataStorage.Save();
        }
    }
}
