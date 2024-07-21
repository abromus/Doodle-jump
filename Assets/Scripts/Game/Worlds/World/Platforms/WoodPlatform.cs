﻿using System;
using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class WoodPlatform : Platform
    {
        [SerializeField] private int _id;
        [SerializeField] private Vector2 _size;

        private IPlatformCollisionInfo _info;

        public override int Id => _id;

        public override Vector2 Size => _size;

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.relativeVelocity.y <= 0f)
                Collided.SafeInvoke(_info);
        }
    }
}
