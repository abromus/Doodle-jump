using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerMovement : IDoodlerMovement
    {
        private bool _isPaused;
        private Vector2 _currentVelocity;
        private float _currentAngularVelocity;

        private readonly Rigidbody2D _rigidbody;
        private readonly IDoodlerGameOverMovementAnimator _animator;
        private readonly IDoodlerVelocity _velocity;
        private readonly IDoodlerJump _jump;
        private readonly Vector2 _zero = Vector2.zero;

        public Vector2 Velocity => _velocity.Velocity;

        public event System.Action Jumped;

        internal DoodlerMovement(in DoodlerMovementArgs args)
        {
            _rigidbody = args.Rigidbody;

            var animationArgs = args.MovementAnimationArgs;
            _animator = new DoodlerGameOverMovementAnimator(_rigidbody, args.CameraService, in animationArgs);
            _velocity = new DoodlerVelocity(in args);
            _jump = new DoodlerJump(in args);
            _jump.Jumped += OnJumped;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Jump(float height)
        {
            _jump.Do(height);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void GameOver(GameOverType type)
        {
            _animator.Start(type);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Restart()
        {
            _animator.End();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            _velocity.Tick();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void FixedTick(float deltaTime)
        {
            _velocity.FixedTick(deltaTime);
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;

            if (_isPaused)
            {
                _currentVelocity = _rigidbody.velocity;
                _currentAngularVelocity = _rigidbody.angularVelocity;
                _rigidbody.isKinematic = true;
                _rigidbody.velocity = _zero;
            }
            else
            {
                _rigidbody.velocity = _currentVelocity;
                _rigidbody.angularVelocity = _currentAngularVelocity;
                _rigidbody.isKinematic = false;
                _currentVelocity = _zero;
            }
        }

        private void OnJumped()
        {
            Jumped.SafeInvoke();
        }
    }
}
