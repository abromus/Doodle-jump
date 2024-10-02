namespace DoodleJump.Game.Settings
{
    internal interface IBoosterConfig : Core.Settings.IConfig
    {
        public string Title { get; }

        public Worlds.Boosters.BoosterType BoosterType { get; }

        public Worlds.Entities.Boosters.BaseBooster BoosterPrefab { get; }
    }
}
