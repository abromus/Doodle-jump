using System.Collections.Generic;
using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    internal interface IPlatformsConfig : IConfig
    {
        public IReadOnlyList<IPlatformConfig> Configs { get; }
    }
}
