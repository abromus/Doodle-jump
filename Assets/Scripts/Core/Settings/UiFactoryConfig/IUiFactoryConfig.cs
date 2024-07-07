using System.Collections.Generic;
using DoodleJump.Core.Factories;

namespace DoodleJump.Core.Settings
{
    public interface IUiFactoryConfig : IConfig
    {
        public IReadOnlyList<IUiFactory> UiFactories { get; }
    }
}
