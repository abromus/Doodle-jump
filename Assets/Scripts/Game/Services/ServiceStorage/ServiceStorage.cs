using System;
using System.Collections.Generic;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Core.Settings;
using DoodleJump.Game.Data;
using DoodleJump.Game.Settings;
using UnityEngine;

namespace DoodleJump.Game.Services
{
    internal sealed class ServiceStorage : IServiceStorage
    {
        private Dictionary<Type, IService> _services;

        private readonly IConfigStorage _configStorage;
        private readonly Transform _uiServicesContainer;

        public Transform UiServiceContainer => _uiServicesContainer;

        internal ServiceStorage(IGameData gameData, IConfigStorage configStorage, Transform uiServicesContainer)
        {
            _configStorage = configStorage;
            _uiServicesContainer = uiServicesContainer;

            var updater = gameData.CoreData.ServiceStorage.GetUpdater();
            var uiServices = _configStorage.GetGameUiServiceConfig().UiServices;
            var audioService = InitAudioService(updater, uiServices, _uiServicesContainer);
            var saveLoadService = InitSaveLoadService(updater);
            var screenSystemService = InitScreenSystemService(uiServices, _uiServicesContainer);

            _services = new(8)
            {
                [typeof(IAudioService)] = audioService,
                [typeof(ISaveLoadService)] = saveLoadService,
                [typeof(IScreenSystemService)] = screenSystemService,
            };
        }

        public void AddService<TService>(TService service) where TService : class, IService
        {
            var type = typeof(TService);

            if (_services.ContainsKey(type))
                _services[type] = service;
            else
                _services.Add(type, service);
        }

        public TService GetService<TService>() where TService : class, IService
        {
            return _services[typeof(TService)] as TService;
        }

        public void Destroy()
        {
            foreach (var service in _services.Values)
                service.Destroy();

            _services.Clear();
            _services = null;
        }

        private IAudioService InitAudioService(IUpdater updater, IReadOnlyList<IUiService> uiServices, Transform container)
        {
            var audioServicePrefab = uiServices.GetAudioService();
            var audioServiceObject = InstantiateUiService(audioServicePrefab as UiService, container);
            var audioService = audioServiceObject as IAudioService;
            audioService.Init(updater);

            return audioService;
        }

        private ISaveLoadService InitSaveLoadService(IUpdater updater)
        {
            var doodlerConfig = _configStorage.GetDoodlerConfig();
            var saveLoadService = new SaveLoadService(updater, doodlerConfig);

            return saveLoadService;
        }

        private IScreenSystemService InitScreenSystemService(IReadOnlyList<IUiService> uiServices, Transform container)
        {
            var screenSystemServicePrefab = uiServices.GetScreenSystemService();
            var screenSystemServiceObject = InstantiateUiService(screenSystemServicePrefab as UiService, container);
            var screenSystemService = screenSystemServiceObject as IScreenSystemService;

            return screenSystemService;
        }

        private UiService InstantiateUiService(UiService uiServicePrefab, Transform container)
        {
            var uiService = UnityEngine.Object.Instantiate(uiServicePrefab, container);
            uiService.gameObject.RemoveCloneSuffix();

            return uiService;
        }
    }
}
