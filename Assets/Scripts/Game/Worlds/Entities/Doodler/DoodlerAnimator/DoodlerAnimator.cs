﻿using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerAnimator : IDoodlerAnimator
    {
        private float _previousVelocity;
        private float _currentVelocity;
        private float _previousDirection;
        private bool _isPaused;

        private readonly Transform _doodler;
        private readonly Animator _animator;
        private readonly IDoodlerMovement _movement;
        private readonly IDoodlerInput _doodlerInput;

        internal DoodlerAnimator(Transform doodler, Animator animator, IDoodlerMovement movement, IDoodlerInput doodlerInput)
        {
            _doodler = doodler;
            _animator = animator;
            _movement = movement;
            _doodlerInput = doodlerInput;
        }

        public void FixedTick(float deltaTime)
        {
            if (_isPaused)
                return;

            _previousVelocity = _currentVelocity;
            _currentVelocity = _movement.Velocity.y;

            SetGrounded(_previousVelocity < 0f && 0f < _currentVelocity);
            CheckMoveDirection();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;

            _animator.speed = _isPaused ? Constants.PauseSpeed : Constants.ActiveSpeed;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void SetGrounded(bool isCollided)
        {
            ChangeTrigger(AnimationKeys.TriggerKeys.Collided, isCollided);
        }

        private void ChangeTrigger(string triggerKey, bool value)
        {
            if (value)
                _animator.SetTrigger(triggerKey);
            else
                _animator.ResetTrigger(triggerKey);
        }

        private void CheckMoveDirection()
        {
            var input = _doodlerInput.MoveDirection.x;

            if (input == 0f)
                return;

            if (0f < input && _previousDirection < 0f)
                SetDirection(AnimationKeys.DirectionKeys.Positive);
            else if (input < 0f && 0f <= _previousDirection)
                SetDirection(AnimationKeys.DirectionKeys.Negative);
        }

        private void SetDirection(float direction)
        {
            _previousDirection = direction;

            var localScale = _doodler.localScale;
            localScale.x = direction;
            _doodler.localScale = localScale;
        }

        private sealed class AnimationKeys
        {
            internal sealed class DirectionKeys
            {
                internal const float Positive = 1f;
                internal const float Negative = -1f;
            }

            internal sealed class TriggerKeys
            {
                internal const string Collided = "Collided";
            }

            internal sealed class AnimationNames
            {
                internal const string JumpAnimation = "Jump";
                internal const string CollidedAnimation = "Collided";
            }
        }
    }
}
