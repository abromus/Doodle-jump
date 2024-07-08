using UnityEngine;

namespace DoodleJump.Game.Entities
{
    internal sealed class DoodlerVelocity : IDoodlerVelocity
    {
        private Vector2 _direction;

        private readonly Rigidbody2D _rigidbody;
        private readonly float _movementVelocity;
        private readonly IDoodlerInput _doodlerInput;

        internal DoodlerVelocity(in DoodlerMovementArgs args)
        {
            _rigidbody = args.Rigidbody;
            _movementVelocity = args.DoodlerConfig.MovementVelocity;
            _doodlerInput = args.DoodlerInput;
        }

        public void Tick()
        {
            CheckInput();
        }

        public void FixedTick(float deltaTime)
        {
            Move(deltaTime);
        }

        private void CheckInput()
        {
            _direction = _doodlerInput.Direction;
        }

        private void Move(float deltaTime)
        {
            _rigidbody.AddForce(_movementVelocity * deltaTime * _direction, ForceMode2D.Impulse);
        }
    }
}
