using System.Collections.Generic;
using DoodleJump.Core.Settings;
using DoodleJump.Game.Services;

namespace DoodleJump.Game.Settings
{
    internal interface IUiServiceConfig : IConfig
    {
        public IReadOnlyList<IUiService> UiServices { get; }
    }
}
