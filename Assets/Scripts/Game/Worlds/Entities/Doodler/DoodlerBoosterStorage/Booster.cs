namespace DoodleJump.Game.Worlds.Entities.Boosters
{
    internal abstract class Booster : UnityEngine.MonoBehaviour, IBooster
    {
        [UnityEngine.SerializeField] private UnityEngine.SpriteRenderer _spriteRenderer;
        [UnityEngine.SerializeField] private Worlds.Boosters.BoosterType _boosterType;

        protected UnityEngine.SpriteRenderer SpriteRenderer => _spriteRenderer;

        public Worlds.Boosters.BoosterType BoosterType => _boosterType;

        public abstract event System.Action<IBooster> Executed;

        public abstract void Init(Settings.IBoosterConfig config);

        public abstract void Execute(Core.Services.IUpdater updater);

        public abstract void Destroy();
    }
}
