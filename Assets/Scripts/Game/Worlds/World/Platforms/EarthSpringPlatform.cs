﻿using System;
using DoodleJump.Core;
using DoodleJump.Game.Services;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class EarthSpringPlatform : BasePlatform
    {
        [SerializeField] private Transform _spring;
        [SerializeField] private float _springSize;
        [SerializeField] private float _springOffset;
        [SerializeField] private Animator _animator;
        [SerializeField] private PlatformClipType _springClipType;

        private SpringJumpPlatformCollisionInfo _info;
        private float _springPosition;
        private Vector2 _springPositionRange;

        public override event Action<IPlatformCollisionInfo> Collided;

        public override event Action<IPlatform> Destroyed;

        public override void InitPosition(Vector3 position)
        {
            base.InitPosition(position);

            UpdateSpringPosition();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            Destroyed.SafeInvoke(this);
        }

        private void Awake()
        {
            _info = new SpringJumpPlatformCollisionInfo(this);

            var offset = (Size.x - _springSize - _springOffset) * Constants.Half;
            _springPositionRange = new Vector2(-offset, offset);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (0f < collision.relativeVelocity.y || collision.transform.TryGetComponent<IDoodler>(out var doodler) == false)
                return;

            var doodlerPosition = doodler.GameObject.transform.position.x;
            var doodlerSize = doodler.Size.x * Constants.Half;
            var isSpringCollided = doodlerPosition - doodlerSize < _springPosition + _springSize && _springPosition - _springSize < doodlerPosition + doodlerSize;
            _info.SetIsSpringCollided(isSpringCollided);

            if (isSpringCollided)
                _animator.SetTrigger(AnimationKeys.TriggerKeys.Collided);

            var clipType = isSpringCollided ? _springClipType : ClipType;

            PlaySound(clipType);

            Collided.SafeInvoke(_info);
        }

        private void UpdateSpringPosition()
        {
            var localPosition = UnityEngine.Random.Range(_springPositionRange.x, _springPositionRange.y);

            var position = _spring.localPosition;
            position.x = localPosition;
            _spring.localPosition = position;

            _springPosition = _spring.position.x + localPosition;
        }

        private sealed class AnimationKeys
        {
            internal sealed class TriggerKeys
            {
                internal const string Collided = "Collided";
            }
        }
    }
}
