namespace DoodleJump.Game.Settings
{
    internal interface IProgressInfo
    {
        public float MinProgress { get; }

        public float MaxProgress { get; }

        public int PlatformMaxCount { get; }

        public int EnemySimultaneouslyCount { get; }

        public int EnemyMaxCount { get; }

        public float EnemySpawnProbability { get; }

        public float EnemySpawnProbabilityFactor { get; }

        public float MinOffsetY { get; }

        public float MaxOffsetY { get; }

        public System.Collections.Generic.IReadOnlyList<IPlatformConfig> PlatformConfigs { get; }

        public System.Collections.Generic.IReadOnlyList<IEnemyConfig> EnemyConfigs { get; }
    }
}
