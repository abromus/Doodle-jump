using System;
using DoodleJump.Core;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class StonePlatform : Platform
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
            _info = new JumpPlatformCollisionInfo(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (0f < collision.relativeVelocity.y || collision.transform.TryGetComponent<IDoodler>(out var doodler) == false)
                return;

            Collided.SafeInvoke(_info);

            PlaySound(ClipType);
        }
    }
}
