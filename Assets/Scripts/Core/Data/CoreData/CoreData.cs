using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;

namespace DoodleJump.Core.Data
{
    internal sealed class CoreData : ICoreData
    {
        private readonly IConfigStorage _configStorage;
        private readonly IServiceStorage _serviceStorage;
        private readonly IFactoryStorage _factoryStorage;

        public IConfigStorage ConfigStorage => _configStorage;

        public IServiceStorage ServiceStorage => _serviceStorage;

        public IFactoryStorage FactoryStorage => _factoryStorage;

        internal CoreData(IConfigStorage configStorage, IUpdater updater, UnityEngine.Transform uiServicesContainer)
        {
            _configStorage = configStorage;
            _serviceStorage = new ServiceStorage(this, configStorage, updater, uiServicesContainer);
            _factoryStorage = new FactoryStorage(configStorage);
        }

        public void Destroy()
        {
            _factoryStorage.Destroy();
            _serviceStorage.Destroy();
        }
    }
}
