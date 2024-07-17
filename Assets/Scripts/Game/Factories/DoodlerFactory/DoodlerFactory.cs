using DoodleJump.Core;
using DoodleJump.Core.Factories;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Factories
{
    internal sealed class DoodlerFactory : UiFactory, IDoodlerFactory
    {
        [SerializeField] private Doodler _doodler;

        private DoodlerArgs _args;

        public override UiFactoryType UiFactoryType => UiFactoryType.DoodlerFactory;

        public void Init(DoodlerArgs args)
        {
            _args = args;
        }

        public IDoodler Create()
        {
            var doodler = Instantiate(_doodler);
            doodler.Init(_args);
            doodler.gameObject.RemoveCloneSuffix();

            return doodler;
        }
    }
}
