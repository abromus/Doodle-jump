using System.Collections.Generic;

namespace DoodleJump.Core.Services
{
    public static class ServiceExtensions
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ICameraService GetCameraService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<ICameraService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static ICameraService GetCameraService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<ICameraService>(UiServiceType.CameraService);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IEventSystemService GetEventSystemService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IEventSystemService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IEventSystemService GetEventSystemService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IEventSystemService>(UiServiceType.EventSystemService);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IInputService GetInputService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IInputService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IStateMachine GetStateMachine(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IStateMachine>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
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
