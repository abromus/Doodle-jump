namespace DoodleJump.Game.Settings
{
    internal interface IGeneratorConfig : Core.Settings.IConfig
    {
        public UnityEngine.Vector3 PlatformsStartPosition { get; }

        public UnityEngine.Vector3 EnemiesStartPosition { get; }

        public UnityEngine.Vector3 BoostersStartPosition { get; }

        public int PlatformStartCount { get; }

        public IProgressInfo[] ProgressInfos { get; }
    }
}
