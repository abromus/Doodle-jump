namespace DoodleJump.Game.UI
{
    internal interface IUiBooster : Core.IPoolable
    {
        public UnityEngine.GameObject GameObject { get; }

        public Worlds.Boosters.BoosterType BoosterType { get; }

        public int Count { get; }

        public event System.Action<IUiBooster> Clicked;

        public void Init(Worlds.Boosters.BoosterType boosterType, int count);

        public void UpdateCount(int count);
    }
}
