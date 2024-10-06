using System;
using System.Collections.Generic;
using DoodleJump.Core.Data;
using DoodleJump.Core.Settings;
using DoodleJump.Core.States;
using UnityEngine;

namespace DoodleJump.Core.Services
{
    internal sealed class ServiceStorage : IServiceStorage
    {
        private Dictionary<Type, IService> _services;

        private readonly IConfigStorage _configStorage;
        private readonly Transform _uiServicesContainer;

        public Transform UiServiceContainer => _uiServicesContainer;

        internal ServiceStorage(ICoreData coreData, IConfigStorage configStorage, IUpdater updater, Transform uiServicesContainer)
        {
            _configStorage = configStorage;
            _uiServicesContainer = uiServicesContainer;

            UnityEngine.Object.DontDestroyOnLoad(_uiServicesContainer);

            var uiServices = _configStorage.GetCoreUiServiceConfig().UiServices;

            var stateMachine = InitStateMachine(coreData);
            var inputService = InitInputService();
            var qualityService = InitQualityService();

            var cameraService = InitCameraService(uiServices, _uiServicesContainer);
            var eventSystemService = InitEventSystemService(uiServices, _uiServicesContainer);

            _services = new(8)
            {
                [typeof(IUpdater)] = updater,
                [typeof(IStateMachine)] = stateMachine,
                [typeof(IInputService)] = inputService,
                [typeof(IQualityService)] = qualityService,
                [typeof(ICameraService)] = cameraService,
                [typeof(IEventSystemService)] = eventSystemService,
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

        private IInputService InitInputService()
        {
#if UNITY_EDITOR == false && UNITY_ANDROID
            var inputService = new AndroidInputService();
#else
            var inputService = new InputService();
#endif

            return inputService;
        }

        private IQualityService InitQualityService()
        {
            var qualityConfig = _configStorage.GetQualityConfig();
            var qualityService = new QualityService(qualityConfig);

            return qualityService;
        }

        private IStateMachine InitStateMachine(ICoreData coreData)
        {
            var stateMachine = new StateMachine();

            stateMachine.Add(new BootstrapState(stateMachine));
            stateMachine.Add(new GameState(stateMachine));
            stateMachine.Add(new SceneLoaderState(new SceneLoader()));
            stateMachine.Add(new GameLoopState(coreData));

            return stateMachine;
        }

        private ICameraService InitCameraService(IReadOnlyList<IUiService> uiServices, Transform container)
        {
            var cameraServicePrefab = uiServices.GetCameraService();
            var cameraServiceObject = InstantiateUiService(cameraServicePrefab as BaseUiService, container);

            var cameraService = cameraServiceObject as ICameraService;
            cameraService.Init(container);

            return cameraService;
        }

        private IEventSystemService InitEventSystemService(IReadOnlyList<IUiService> uiServices, Transform container)
        {
            var eventSystemServicePrefab = uiServices.GetEventSystemService();
            var eventSystemService = InstantiateUiService(eventSystemServicePrefab as BaseUiService, container);

            return eventSystemService as IEventSystemService;
        }

        private BaseUiService InstantiateUiService(BaseUiService uiServicePrefab, Transform container)
        {
            var uiService = UnityEngine.Object.Instantiate(uiServicePrefab, container);
            uiService.gameObject.RemoveCloneSuffix();

            return uiService;
        }
    }
}
