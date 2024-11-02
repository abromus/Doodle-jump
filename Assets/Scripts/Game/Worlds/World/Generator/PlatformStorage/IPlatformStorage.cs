namespace DoodleJump.Game.Worlds
{
    internal interface IPlatformStorage : Core.IDestroyable
    {
        public float HighestPlatformY { get; }

        public System.Collections.Generic.IReadOnlyList<Platforms.IPlatform> Platforms { get; }

        public event System.Action<Settings.IProgressInfo, Platforms.IPlatformCollisionInfo> Collided;

        public void Clear();

        public void GenerateStartPlatform();

        public void GeneratePlatforms();

        public void DestroyPlatform(Platforms.IPlatform platform);
    }
}
