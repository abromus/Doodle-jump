using DoodleJump.Game.Worlds.Boosters;

namespace DoodleJump.Game.Settings
{
    internal interface IBoosterConfig : IProbable
    {
        public string Title { get; }

        public Booster BoosterPrefab { get; }

        public BoosterTriggerType TriggerType { get; }
    }
}
