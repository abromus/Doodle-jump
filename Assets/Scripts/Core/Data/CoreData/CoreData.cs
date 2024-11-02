namespace DoodleJump.Core.Data
{
    internal sealed class CoreData : ICoreData
    {
        private readonly Settings.IConfigStorage _configStorage;
        private readonly Services.IServiceStorage _serviceStorage;
        private readonly Factories.IFactoryStorage _factoryStorage;

        public Settings.IConfigStorage ConfigStorage => _configStorage;

        public Services.IServiceStorage ServiceStorage => _serviceStorage;

        public Factories.IFactoryStorage FactoryStorage => _factoryStorage;

        internal CoreData(Settings.IConfigStorage configStorage, Services.IUpdater updater, UnityEngine.Transform uiServicesContainer)
        {
            _configStorage = configStorage;
            _serviceStorage = new Services.ServiceStorage(this, configStorage, updater, uiServicesContainer);
            _factoryStorage = new Factories.FactoryStorage(configStorage);
        }

        public void Destroy()
        {
            _factoryStorage.Destroy();
            _serviceStorage.Destroy();
        }
    }
}
