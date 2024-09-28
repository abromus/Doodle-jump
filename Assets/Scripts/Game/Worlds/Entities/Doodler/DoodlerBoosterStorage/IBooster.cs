namespace DoodleJump.Game.Worlds.Entities.Boosters
{
    internal interface IBooster
    {
        public Worlds.Boosters.BoosterType BoosterType { get; }

        public event System.Action<IBooster> Executed;

        public abstract void Init(Settings.IBoosterConfig config, in DoodlerBoosterStorageArgs args);

        public void Execute();

        public void Destroy();
    }
}
