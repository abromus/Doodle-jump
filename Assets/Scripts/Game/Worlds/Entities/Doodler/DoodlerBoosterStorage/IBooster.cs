namespace DoodleJump.Game.Worlds.Entities.Boosters
{
    internal interface IBooster
    {
        public Worlds.Boosters.BoosterType BoosterType { get; }

        public event System.Action<IBooster> Executed;

        public abstract void Init(Settings.IBoosterConfig config);

        public void Execute(Core.Services.IUpdater updater);

        public void Destroy();
    }
}
