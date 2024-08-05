namespace DoodleJump.Game.Settings
{
    internal interface IProgressInfo
    {
        public float MinProgress { get; }

        public float MaxProgress { get; }

        public int PlatformMaxCount { get; }

        public float MinOffsetY { get; }

        public float MaxOffsetY { get; }

        public System.Collections.Generic.IReadOnlyList<IPlatformConfig> PlatformConfigs { get; }
    }
}
