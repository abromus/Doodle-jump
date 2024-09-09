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
        private readonly IServiceStorage _serviceStorage;
        private readonly IFactoryStorage _factoryStorage;

        public ICoreData CoreData => _coreData;

        public IConfigStorage ConfigStorage => _configStorage;

        public IServiceStorage ServiceStorage => _serviceStorage;

        public IFactoryStorage FactoryStorage => _factoryStorage;

        internal GameData(ICoreData coreData, IConfigStorage configStorage, UnityEngine.Transform uiServicesContainer)
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
