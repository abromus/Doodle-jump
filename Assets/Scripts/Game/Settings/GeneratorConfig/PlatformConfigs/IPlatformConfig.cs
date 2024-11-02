namespace DoodleJump.Game.Settings
{
    internal interface IPlatformConfig : IProbable
    {
        public string Title { get; }

        public Worlds.Platforms.BasePlatform PlatformPrefab { get; }

        public Worlds.Platforms.PlatformTriggerType TriggerType { get; }
    }
}
