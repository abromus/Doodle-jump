using System.Collections.Generic;
using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    internal interface IEnemiesConfig : IConfig
    {
        public IReadOnlyList<IEnemyConfig> Configs { get; }
    }
}
