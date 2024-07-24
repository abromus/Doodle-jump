using UnityEngine;

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
        private readonly float _activeSpeed = 1f;
        private readonly float _pauseSpeed = 0f;

        public DoodlerAnimator(Transform doodler, Animator animator, IDoodlerMovement movement, IDoodlerInput doodlerInput)
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
            CheckDirection();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;

            _animator.speed = _isPaused ? _pauseSpeed : _activeSpeed;
        }

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

        private void CheckDirection()
        {
            var input = _doodlerInput.Direction.x;

            if (input == AnimationKeys.DirectionKeys.Neutral)
                return;

            if (input == AnimationKeys.DirectionKeys.Positive && _previousDirection == AnimationKeys.DirectionKeys.Negative)
                SetDirection(AnimationKeys.DirectionKeys.Positive);
            else if (input == AnimationKeys.DirectionKeys.Negative && (_previousDirection == AnimationKeys.DirectionKeys.Positive || _previousDirection == AnimationKeys.DirectionKeys.Neutral))
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
                internal const float Neutral = 0f;
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
