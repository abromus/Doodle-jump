using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerMovement : IDoodlerMovement
    {
        private bool _isPaused;
        private Vector2 _currentVelocity;
        private float _currentAngularVelocity;

        private readonly Rigidbody2D _rigidbody;
        private readonly IDoodlerVelocity _velocity;
        private readonly IDoodlerJump _jump;
        private readonly Vector2 _zero = Vector2.zero;

        public Vector2 Velocity => _velocity.Velocity;

        internal DoodlerMovement(in DoodlerMovementArgs args)
        {
            _rigidbody = args.Rigidbody;

            _velocity = new DoodlerVelocity(in args);
            _jump = new DoodlerJump(in args);
        }

        public void Jump(float height)
        {
            _jump.Do(height);
        }

        public void Tick(float deltaTime)
        {
            _velocity.Tick();
        }

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
    }
}
