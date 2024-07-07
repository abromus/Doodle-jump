using System.Collections.Generic;
using DoodleJump.Core.Services;

namespace DoodleJump.Core.Settings
{
    public interface IKeymapConfig : IConfig
    {
        public IDictionary<KeyName, ButtonInfo> GetButtonInfos(bool force = false);
    }
}
