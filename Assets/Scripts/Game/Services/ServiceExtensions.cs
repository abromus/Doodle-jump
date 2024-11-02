namespace DoodleJump.Game.Services
{
    internal static class ServiceExtensions
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IAudioService GetAudioService(this Core.Services.IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IAudioService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IAudioService GetAudioService(this System.Collections.Generic.IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IAudioService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static ISaveLoadService GetSaveLoadService(this Core.Services.IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<ISaveLoadService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IScreenSystemService GetScreenSystemService(this Core.Services.IServiceStorage serviceStorage)
        {
            return serviceStorage.GetService<IScreenSystemService>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IScreenSystemService GetScreenSystemService(this System.Collections.Generic.IReadOnlyList<IUiService> uiServices)
        {
            return uiServices.GetService<IScreenSystemService>();
        }

        internal static TService GetService<TService>(this System.Collections.Generic.IReadOnlyList<IUiService> uiServices) where TService : class, Core.Services.IService
        {
            foreach (var uiService in uiServices)
                if (uiService is TService service)
                    return service;

            return null;
        }
    }
}
