using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerAnimator : IDoodlerAnimator
    {
        private float _previousVelocity;
        private float _currentVelocity;

        private readonly Animator _animator;
        private readonly IDoodlerMovement _movement;

        public DoodlerAnimator(Animator animator, IDoodlerMovement movement)
        {
            _animator = animator;
            _movement = movement;
        }

        public void FixedTick(float deltaTime)
        {
            _previousVelocity = _currentVelocity;
            _currentVelocity = _movement.Velocity.y;

            SetGrounded(_previousVelocity < 0f && 0f < _currentVelocity);
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

        private sealed class AnimationKeys
        {
            internal sealed class TriggerKeys
            {
                internal const string Collided = "Collided";
            }
        }
    }
}
