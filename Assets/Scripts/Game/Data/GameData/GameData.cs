namespace DoodleJump.Game.Data
{
    internal sealed class GameData : IGameData
    {
        private readonly Core.Data.ICoreData _coreData;
        private readonly Core.Settings.IConfigStorage _configStorage;
        private readonly Core.Services.IServiceStorage _serviceStorage;
        private readonly Core.Factories.IFactoryStorage _factoryStorage;

        public Core.Data.ICoreData CoreData => _coreData;

        public Core.Settings.IConfigStorage ConfigStorage => _configStorage;

        public Core.Services.IServiceStorage ServiceStorage => _serviceStorage;

        public Core.Factories.IFactoryStorage FactoryStorage => _factoryStorage;

        internal GameData(Core.Data.ICoreData coreData, Core.Settings.IConfigStorage configStorage, UnityEngine.Transform uiServicesContainer)
        {
            _coreData = coreData;
            _configStorage = configStorage;
            _serviceStorage = new Services.ServiceStorage(this, configStorage, uiServicesContainer);
            _factoryStorage = new Factories.FactoryStorage(this, _coreData, configStorage, _serviceStorage);
        }

        public void Destroy()
        {
            _factoryStorage.Destroy();
            _serviceStorage.Destroy();
        }
    }
}
