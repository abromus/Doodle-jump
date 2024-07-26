namespace DoodleJump.Core.Settings
{
    public static class ConfigExtensions
    {
        public static IInputConfig GetInputConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IInputConfig>();
        }

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
