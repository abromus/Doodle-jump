using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using UnityEngine;

namespace DoodleJump.Core.Data
{
    internal sealed class CoreData : ICoreData
    {
        private readonly IConfigStorage _configStorage;
        private readonly IFactoryStorage _factoryStorage;
        private readonly IServiceStorage _serviceStorage;

        public IConfigStorage ConfigStorage => _configStorage;

        public IFactoryStorage FactoryStorage => _factoryStorage;

        public IServiceStorage ServiceStorage => _serviceStorage;

        internal CoreData(IConfigStorage configStorage, IUpdater updater, Transform uiServicesContainer)
        {
            _configStorage = configStorage;
            _factoryStorage = new FactoryStorage(configStorage);
            _serviceStorage = new ServiceStorage(this, configStorage, updater, uiServicesContainer);
        }

        public void Destroy()
        {
            _factoryStorage.Destroy();
            _serviceStorage.Destroy();
        }
    }
}
