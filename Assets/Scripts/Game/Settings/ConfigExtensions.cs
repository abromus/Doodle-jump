using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    internal static class ConfigExtensions
    {
        internal static ICameraConfig GetCameraConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<ICameraConfig>();
        }

        internal static IDoodlerConfig GetDoodlerConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IDoodlerConfig>();
        }

        internal static IUiFactoryConfig GetGameUiFactoryConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IUiFactoryConfig>();
        }

        public static IUiServiceConfig GetGameUiServiceConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IUiServiceConfig>();
        }

        internal static IGeneratorConfig GetGeneratorConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IGeneratorConfig>();
        }

        internal static IPlatformsConfig GetPlatformsConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IPlatformsConfig>();
        }
    }
}
