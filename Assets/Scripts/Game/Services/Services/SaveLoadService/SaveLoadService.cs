using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using Mono.Data.Sqlite;

namespace DoodleJump.Game.Services
{
    internal sealed class SaveLoadService : ISaveLoadService, ILateUpdatable
    {
        private readonly IUpdater _updater;
        private readonly IPersistentDataStorage _persistentDataStorage;
        private readonly SqliteConnection _connection;

        public IPersistentDataStorage PersistentDataStorage => _persistentDataStorage;

        internal SaveLoadService(IUpdater updater)
        {
            _updater = updater;

            _connection = new SqliteConnection($"URI=file:{UnityEngine.Application.persistentDataPath}/SaveLoadService.db");
            _connection.Open();

            _persistentDataStorage = new PersistentDataStorage();
            _persistentDataStorage.Init(_connection);

            _updater.AddLateUpdatable(this);
        }

        public void LateTick(float deltaTime)
        {
            Save();
        }

        public void Destroy()
        {
            Save();

            _persistentDataStorage.Dispose();
            _connection.Close();

            _updater.RemoveLateUpdatable(this);
        }

        private void Save()
        {
            _persistentDataStorage.Save();
        }
    }
}
