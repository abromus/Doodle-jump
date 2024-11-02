namespace DoodleJump.Game.Settings
{
    internal interface IWorldBoosterConfig : IProbable
    {
        public string Title { get; }

        public Worlds.Boosters.BaseWorldBooster WorldBoosterPrefab { get; }

        public Worlds.Boosters.BoosterTriggerType TriggerType { get; }
    }
}
