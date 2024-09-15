using DoodleJump.Core.Settings;
using UnityEngine;

namespace DoodleJump.Game.Settings
{
    internal interface IGeneratorConfig : IConfig
    {
        public Vector3 PlatformsStartPosition { get; }

        public Vector3 EnemiesStartPosition { get; }

        public Vector3 BoostersStartPosition { get; }

        public int PlatformStartCount { get; }

        public IProgressInfo[] ProgressInfos { get; }
    }
}
