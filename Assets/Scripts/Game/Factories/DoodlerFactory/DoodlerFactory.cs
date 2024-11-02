using DoodleJump.Core;

namespace DoodleJump.Game.Factories
{
    internal sealed class DoodlerFactory : Core.Factories.BaseUiFactory, IDoodlerFactory
    {
        [UnityEngine.SerializeField] private Worlds.Entities.Doodler _doodler;

        private Worlds.Entities.DoodlerArgs _args;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Init(in Worlds.Entities.DoodlerArgs args)
        {
            _args = args;
        }

        public Worlds.Entities.IDoodler Create()
        {
            var doodler = InstantiateDoodler(_doodler);
            doodler.Init(in _args);

            return doodler;
        }

        private Worlds.Entities.IDoodler InstantiateDoodler<T>(T prefab) where T : UnityEngine.MonoBehaviour, Worlds.Entities.IDoodler
        {
            var doodler = Instantiate(prefab);
            doodler.gameObject.RemoveCloneSuffix();

            return doodler;
        }
    }
}
