using System.Collections.Generic;
using DoodleJump.Core.Services;

namespace DoodleJump.Core.Settings
{
    public interface IUiServiceConfig : IConfig
    {
        public IReadOnlyList<IUiService> UiServices { get; }
    }
}
