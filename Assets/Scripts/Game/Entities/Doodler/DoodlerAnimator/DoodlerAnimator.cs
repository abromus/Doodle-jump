using UnityEngine;

namespace DoodleJump.Game.Entities
{
    internal sealed class DoodlerAnimator : IDoodlerAnimator
    {
        private readonly Animator _animator;
        private readonly IDoodlerMovement _movement;

        public DoodlerAnimator(Animator animator, IDoodlerMovement movement)
        {
            _animator = animator;
            _movement = movement;
        }

        public void FixedTick(float deltaTime)
        {
            /*var magnitude = _movement.GetHorizontalVelocity().magnitude;

            if (magnitude < 0.01f)
                magnitude = 0f;

            SetGrounded(magnitude);*/
        }

        private void SetGrounded(bool isGrounded)
        {
            ChangeTrigger(AnimationKeys.TriggerKeys.IsGrounded, isGrounded);
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
                internal const string IsGrounded = "IsGrounded";
            }
        }
    }
}
