namespace DoodleJump.Core.Settings
{
    internal static class ConfigExtensions
    {
        internal static IUiFactoryConfig GetCoreUiFactoryConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IUiFactoryConfig>();
        }

        internal static IUiServiceConfig GetCoreUiServiceConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IUiServiceConfig>();
        }
    }
}
