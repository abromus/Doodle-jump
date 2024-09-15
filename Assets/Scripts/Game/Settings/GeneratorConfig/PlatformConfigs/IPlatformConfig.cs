using DoodleJump.Game.Worlds.Platforms;

namespace DoodleJump.Game.Settings
{
    internal interface IPlatformConfig : IProbable
    {
        public string Title { get; }

        public Platform PlatformPrefab { get; }

        public PlatformTriggerType TriggerType { get; }
    }
}
