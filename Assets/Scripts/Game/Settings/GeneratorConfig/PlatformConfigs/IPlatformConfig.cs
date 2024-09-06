using DoodleJump.Game.Worlds.Platforms;

namespace DoodleJump.Game.Settings
{
    internal interface IPlatformConfig
    {
        public string Title { get; }

        public Platform PlatformPrefab { get; }

        public float SpawnChance { get; }

        public PlatformTriggerType TriggerType { get; }

#if UNITY_EDITOR
        public void ChangeSpawnChance(float factor);
#endif
    }
}
