using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Settings
{
    internal interface IWorldBoosterConfig : IProbable
    {
        public string Title { get; }

        public WorldBooster WorldBoosterPrefab { get; }

        public BoosterTriggerType TriggerType { get; }
    }
}
