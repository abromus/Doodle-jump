using System;
using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class MoneyWorldBooster : WorldBooster
    {
        [UnityEngine.SerializeField] private int _money;

        private IBoosterCollisionInfo _info;

        public override event Action<IBoosterCollisionInfo> Collided;

        public override event Action<IWorldBooster> Destroyed;

        public override void Destroy()
        {
            base.Destroy();

            Destroyed.SafeInvoke(this);
        }

        protected override void Awake()
        {
            base.Awake();

            _info = new MoneyCollisionInfo(this, _money);
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            Collided.SafeInvoke(_info);

            PlaySound(ClipType);
        }
    }
}
