using System.Collections.Generic;
using DoodleJump.Core.Services;

namespace DoodleJump.Game.Services
{
    internal static class ServiceExtensions
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IAudioService GetAudioService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IAudioService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IAudioService GetAudioService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IAudioService>(UiServiceType.AudioService);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static ISaveLoadService GetSaveLoadService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<ISaveLoadService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IScreenSystemService GetScreenSystemService(this IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IScreenSystemService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IScreenSystemService GetScreenSystemService(this IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IScreenSystemService>(UiServiceType.ScreenSystemService);
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
