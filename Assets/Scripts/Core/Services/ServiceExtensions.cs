using System.Collections.Generic;

namespace DoodleJump.Core.Services
{
    public static class ServiceExtensions
    {
        public static ICameraService GetCameraService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<ICameraService>();
        }

        public static ICameraService GetCameraService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<ICameraService>(UiServiceType.CameraService);
        }

        public static IEventSystemService GetEventSystemService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IEventSystemService>();
        }

        public static IEventSystemService GetEventSystemService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IEventSystemService>(UiServiceType.EventSystemService);
        }

        public static IInputService GetInputService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IInputService>();
        }

        public static IStateMachine GetStateMachine(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IStateMachine>();
        }

        public static IUpdater GetUpdater(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IUpdater>();
        }

        public static TService GetService<TService>(this IReadOnlyList<IUiService> uiServices, UiServiceType serviceType) where TService : class, IService
        {
            foreach (var service in uiServices)
                if (service.UiServiceType == serviceType)
                    return service as TService;

            return null;
        }
    }
}
