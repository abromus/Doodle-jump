using DoodleJump.Core.Data;
using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Data
{
    internal sealed class GameData : IGameData
    {
        private readonly ICoreData _coreData;
        private readonly IConfigStorage _configStorage;
        private readonly IFactoryStorage _factoryStorage;
        private readonly IPersistentDataStorage _persistentDataStorage;
        private readonly IServiceStorage _serviceStorage;

        public ICoreData CoreData => _coreData;

        public IConfigStorage ConfigStorage => _configStorage;

        public IFactoryStorage FactoryStorage => _factoryStorage;

        public IPersistentDataStorage PersistentDataStorage => _persistentDataStorage;

        public IServiceStorage ServiceStorage => _serviceStorage;

        internal GameData(ICoreData coreData, IConfigStorage configStorage, UnityEngine.Transform uiServicesContainer)
        {
            _coreData = coreData;
            _configStorage = configStorage;
            _persistentDataStorage = new PersistentDataStorage();
            _serviceStorage = new Services.ServiceStorage(this, configStorage, uiServicesContainer);
            _factoryStorage = new Factories.FactoryStorage(_coreData, _persistentDataStorage, configStorage, _serviceStorage);
        }

        public void Destroy()
        {
            _factoryStorage.Destroy();
            _serviceStorage.Destroy();
        }
    }
}
