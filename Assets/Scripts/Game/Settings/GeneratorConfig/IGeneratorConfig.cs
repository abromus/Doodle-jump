using DoodleJump.Core.Settings;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    internal interface IGeneratorConfig : IConfig
    {
        public Vector3 StartPosition { get; }

        public int PlatformStartCount { get; }

        public IProgressInfo[] ProgressInfos { get; }
    }
}
