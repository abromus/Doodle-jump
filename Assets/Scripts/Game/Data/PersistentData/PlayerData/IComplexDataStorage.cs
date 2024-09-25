namespace DoodleJump.Game.Data
{
    internal interface IComplexDataStorage : IDataStorage
    {
        public System.Collections.Generic.Dictionary<Worlds.Boosters.BoosterType, int> Boosters { get; }

        public event System.Action<Worlds.Boosters.BoosterType, int> BoosterChanged;

        public void AddBooster(Worlds.Boosters.IBoosterCollisionInfo info, int count);

        public void UseBooster(Worlds.Boosters.BoosterType boosterType, int count);
    }
}
