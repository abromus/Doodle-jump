﻿using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerJump : IDoodlerJump
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;

        public event System.Action Jumped;

        internal DoodlerJump(in DoodlerMovementArgs args)
        {
            _transform = args.Transform;
            _rigidbody = args.Rigidbody;
        }

        public void Do(float height)
        {
            _rigidbody.AddForce(_transform.up * height, ForceMode2D.Impulse);

            Jumped.SafeInvoke();
        }
    }
}
