namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerBoosterStorage : Core.IDestroyable
    {
        public void Add(Worlds.Boosters.IBoosterCollisionInfo info, int count = 1);

        public bool Has(Worlds.Boosters.BoosterType boosterType);
    }
}
