namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class ShieldCollisionInfo : IBoosterCollisionInfo
    {
        private readonly IBooster _booster;

        public IBooster Booster => _booster;

        public ShieldCollisionInfo(IBooster booster)
        {
            _booster = booster;
        }
    }
}
