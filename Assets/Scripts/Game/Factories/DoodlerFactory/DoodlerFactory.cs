using DoodleJump.Core;
using DoodleJump.Core.Factories;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Factories
{
    internal sealed class DoodlerFactory : BaseUiFactory, IDoodlerFactory
    {
        [SerializeField] private Doodler _doodler;

        private DoodlerArgs _args;

        public override UiFactoryType UiFactoryType => UiFactoryType.DoodlerFactory;

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Init(DoodlerArgs args)
        {
            _args = args;
        }

        public IDoodler Create()
        {
            var doodler = InstantiateDoodler(_doodler);
            doodler.Init(_args);

            return doodler;
        }

        private IDoodler InstantiateDoodler<T>(T prefab) where T : MonoBehaviour, IDoodler
        {
            var doodler = Instantiate(prefab);
            doodler.gameObject.RemoveCloneSuffix();

            return doodler;
        }
    }
}
