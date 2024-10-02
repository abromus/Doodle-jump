namespace DoodleJump.Game.Worlds.Boosters
{
    internal readonly struct CollectableTrigger : IBoosterTrigger
    {
        private readonly Entities.IDoodler _doodler;
        private readonly IBoosterCollisionInfo _info;

        public readonly BoosterTriggerType TriggerType => BoosterTriggerType.Collectable;

        internal CollectableTrigger(Entities.IDoodler doodler, IBoosterCollisionInfo info)
        {
            _doodler = doodler;
            _info = info;
        }

        public readonly void Execute()
        {
            _doodler.AddBooster(_info);
            _info.WorldBooster.Destroy();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateInfo(IBoosterCollisionInfo info) { }
    }
}
