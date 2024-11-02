namespace DoodleJump.Game.Worlds
{
    internal interface IBoosterStorage : Core.IDestroyable
    {
        public System.Collections.Generic.IReadOnlyList<Boosters.IWorldBooster> WorldBoosters { get; }

        public event System.Action<Settings.IProgressInfo, Boosters.IBoosterCollisionInfo> Collided;

        public void Clear();

        public void GenerateBoosters();

        public void DestroyBooster(Boosters.IWorldBooster booster);
    }
}
