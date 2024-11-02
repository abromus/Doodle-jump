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
        public static ICameraService GetCameraService(this System.Collections.Generic.IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<ICameraService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IEventSystemService GetEventSystemService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IEventSystemService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IEventSystemService GetEventSystemService(this System.Collections.Generic.IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IEventSystemService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IInputService GetInputService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IInputService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public static IQualityService GetQualityService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IQualityService>();
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

        public static TService GetService<TService>(this System.Collections.Generic.IReadOnlyList<IUiService> uiServices) where TService : class, IService
        {
            foreach (var uiService in uiServices)
                if (uiService is TService service)
                    return service;

            return null;
        }
    }
}
