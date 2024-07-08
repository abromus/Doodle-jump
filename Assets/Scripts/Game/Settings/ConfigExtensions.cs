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
    }
}
