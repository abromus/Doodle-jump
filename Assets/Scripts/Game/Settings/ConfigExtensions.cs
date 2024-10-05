using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    internal static class ConfigExtensions
    {
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IAudioConfig GetAudioConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IAudioConfig>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IBoostersConfig GetBoostersConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IBoostersConfig>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static ICameraConfig GetCameraConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<ICameraConfig>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IDoodlerConfig GetDoodlerConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IDoodlerConfig>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IUiFactoryConfig GetGameUiFactoryConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IUiFactoryConfig>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IUiServiceConfig GetGameUiServiceConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IUiServiceConfig>();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        internal static IGeneratorConfig GetGeneratorConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IGeneratorConfig>();
        }
    }
}
