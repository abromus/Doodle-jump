using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    internal interface IGeneratorConfig : IConfig
    {
        public int PlatformCount { get; }

        public float MinY { get; }

        public float MaxY { get; }
    }
}
