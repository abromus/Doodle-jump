using System.Collections.Generic;

namespace DoodleJump.Core.Services
{
    internal static class ServiceExtensions
    {
        internal static ICameraService GetCameraService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<ICameraService>();
        }

        internal static ICameraService GetCameraService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<ICameraService>(UiServiceType.CameraService);
        }

        internal static IEventSystemService GetEventSystemService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IEventSystemService>();
        }

        internal static IEventSystemService GetEventSystemService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IEventSystemService>(UiServiceType.EventSystemService);
        }

        internal static IInputService GetInputService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IInputService>();
        }

        internal static IStateMachine GetStateMachine(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IStateMachine>();
        }

        internal static IUpdater GetUpdater(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IUpdater>();
        }

        internal static TService GetService<TService>(this IReadOnlyList<IUiService> uiServices, UiServiceType serviceType) where TService : class, IService
        {
            foreach (var service in uiServices)
                if (service.UiServiceType == serviceType)
                    return service as TService;

            return null;
        }
    }
}
