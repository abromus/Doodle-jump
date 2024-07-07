using DoodleJump.Core.Factories;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using UnityEngine;

namespace DoodleJump.Core
{
    internal sealed class GameData : IGameData
    {
        private readonly IConfigStorage _configStorage;
        private readonly IFactoryStorage _factoryStorage;
        private readonly IServiceStorage _serviceStorage;

        public IConfigStorage ConfigStorage => _configStorage;

        public IFactoryStorage FactoryStorage => _factoryStorage;

        public IServiceStorage ServiceStorage => _serviceStorage;

        internal GameData(IConfigStorage configStorage, IUpdater updater, Transform uiServicesContainer)
        {
            _configStorage = configStorage;

            _factoryStorage = new FactoryStorage(configStorage);

            _serviceStorage = new ServiceStorage(this, configStorage, _factoryStorage, updater, uiServicesContainer);
        }

        public void Destroy()
        {
            _factoryStorage.Destroy();

            _serviceStorage.Destroy();
        }
    }
}
