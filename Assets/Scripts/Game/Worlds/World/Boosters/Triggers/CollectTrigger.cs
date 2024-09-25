namespace DoodleJump.Game.Worlds.Boosters
{
    internal readonly struct CollectTrigger : IBoosterTrigger
    {
        private readonly Entities.IDoodler _doodler;
        private readonly IBoosterCollisionInfo _info;

        public readonly BoosterTriggerType TriggerType => BoosterTriggerType.Collect;

        public CollectTrigger(Entities.IDoodler doodler, IBoosterCollisionInfo info)
        {
            _doodler = doodler;
            _info = info;
        }

        public readonly void Execute()
        {
            _doodler.AddBooster(_info);
            _info.WorldBooster.Destroy();
        }

        public void UpdateInfo(IBoosterCollisionInfo info) { }
    }
}
