namespace DoodleJump.Game.Worlds.Boosters
{
    internal interface IBoosterCollisionInfo
    {
        public IWorldBooster WorldBooster { get; }

        public Settings.IBoosterConfig Config { get; }
    }
}
