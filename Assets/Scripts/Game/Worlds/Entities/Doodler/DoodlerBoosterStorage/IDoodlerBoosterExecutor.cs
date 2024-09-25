namespace DoodleJump.Game.Worlds.Entities
{
    internal interface IDoodlerBoosterExecutor : Core.IDestroyable
    {
        public bool Execute(Worlds.Boosters.BoosterType boosterType);

        public bool Has(Worlds.Boosters.BoosterType boosterType);
    }
}
