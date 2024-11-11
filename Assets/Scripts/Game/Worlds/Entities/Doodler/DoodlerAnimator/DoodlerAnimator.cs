namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerAnimator : IDoodlerAnimator
    {
        private float _previousVelocity;
        private float _currentVelocity;
        private float _previousDirection;
        private bool _isPaused;

        private readonly UnityEngine.Transform _doodler;
        private readonly UnityEngine.Animator _animator;
        private readonly IDoodlerInput _doodlerInput;
        private readonly IDoodlerMovement _doodlerMovement;

        private readonly IDoodlerHeadAnimator _headAnimator;

        internal DoodlerAnimator(in DoodlerAnimatorArgs args)
        {
            _doodler = args.DoodlerTransform;
            _animator = args.DoodlerAnimator;
            _doodlerInput = args.DoodlerInput;
            _doodlerMovement = args.DoodlerMovement;

            var doodlerHeadAnimatorArgs = new DoodlerHeadAnimatorArgs(
                args.Head,
                args.ShootingHead,
                _doodlerInput,
                args.PlayerData,
                args.DoodlerConfig.ShootModeDuration);

            _headAnimator = new DoodlerHeadAnimator(in doodlerHeadAnimatorArgs);
        }

        public void Restart()
        {
            _headAnimator.Restart();
        }

        public void Tick(float deltaTime)
        {
            if (_isPaused)
                return;

            _headAnimator.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            if (_isPaused)
                return;

            _previousVelocity = _currentVelocity;
            _currentVelocity = _doodlerMovement.Velocity.y;

            SetGrounded(_previousVelocity < 0f && 0f < _currentVelocity);
            CheckMoveDirection();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;

            _animator.speed = _isPaused ? Constants.PauseSpeed : Constants.ActiveSpeed;

            _headAnimator.SetPause(isPaused);
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
