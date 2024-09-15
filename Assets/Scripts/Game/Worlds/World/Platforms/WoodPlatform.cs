using System;
using DoodleJump.Core;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class WoodPlatform : Platform
    {
        private IPlatformCollisionInfo _info;

        public override event Action<IPlatformCollisionInfo> Collided;

        public override event Action<IPlatform> Destroyed;

        public override void Destroy()
        {
            Destroyed.SafeInvoke(this);
        }

        private void Awake()
        {
            _info = new DestroyedPlatformCollisionInfo(this);
        }

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
            if (0f < collision.relativeVelocity.y)
                return;

            Collided.SafeInvoke(_info);

            PlaySound(ClipType);
        }
    }
}
