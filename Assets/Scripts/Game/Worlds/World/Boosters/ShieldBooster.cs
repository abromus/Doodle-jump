using System;
using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Boosters
{
    internal sealed class ShieldBooster : Booster
    {
        private IBoosterCollisionInfo _info;

        public override event Action<IBoosterCollisionInfo> Collided;

        public override event Action<IBooster> Destroyed;

        public override void Destroy()
        {
            base.Destroy();

            Destroyed.SafeInvoke(this);
        }

        private void Awake()
        {
            _info = new ShieldCollisionInfo(this);
        }

        private void OnTriggerEnter2D(UnityEngine.Collider2D collision)
        {
            Collided.SafeInvoke(_info);

            PlaySound(ClipType);
        }
    }
}
