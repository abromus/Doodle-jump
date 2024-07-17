using DoodleJump.Core.Settings;
using DoodleJump.Game.Worlds;

namespace DoodleJump.Game.Settings
{
    internal interface IGeneratorConfig : IConfig
    {
        public Platform PlatformPrefab { get; }

        public int PlatformCount { get; }

        public float MinY { get; }

        public float MaxY { get; }
    }
}
