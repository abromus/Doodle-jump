namespace DoodleJump.Game.Worlds.Boosters
{
    internal readonly struct ShieldTrigger : IBoosterTrigger
    {
        private readonly Entities.IDoodler _doodler;
        private readonly IBooster _booster;

        public readonly BoosterTriggerType TriggerType => BoosterTriggerType.Shield;

        public ShieldTrigger(Entities.IDoodler doodler, IBooster booster)
        {
            _doodler = doodler;
            _booster = booster;
        }

        public readonly void Execute()
        {
            _doodler.AddBooster(_booster);
            _booster.Destroy();
        }

        public void UpdateInfo(IBoosterCollisionInfo info) { }
    }
}
