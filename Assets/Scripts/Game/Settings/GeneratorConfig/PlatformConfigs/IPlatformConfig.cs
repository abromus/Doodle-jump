using DoodleJump.Game.Worlds.Platforms;

namespace DoodleJump.Game.Settings
{
    internal interface IPlatformConfig : IProbable
    {
        public string Title { get; }

        public BasePlatform PlatformPrefab { get; }

        public PlatformTriggerType TriggerType { get; }
    }
}
