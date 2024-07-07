namespace DoodleJump.Core.Settings
{
    public static class ConfigExtensions
    {
        public static IKeymapConfig GetKeymapConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IKeymapConfig>();
        }

        public static IUiFactoryConfig GetUiFactoryConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IUiFactoryConfig>();
        }

        public static IUiServiceConfig GetUiServiceConfig(this IConfigStorage configStorage)
        {
            return configStorage.GetConfig<IUiServiceConfig>();
        }
    }
}
