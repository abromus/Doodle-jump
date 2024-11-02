namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerVelocity : IDoodlerVelocity
    {
        private UnityEngine.Vector2 _direction;

        private readonly UnityEngine.Rigidbody2D _rigidbody;
        private readonly float _movementVelocity;
        private readonly IDoodlerInput _doodlerInput;

        public UnityEngine.Vector2 Velocity => _rigidbody.velocity;

        internal DoodlerVelocity(in DoodlerMovementArgs args)
        {
            _rigidbody = args.Rigidbody;
            _movementVelocity = args.DoodlerConfig.MovementVelocity;
            _doodlerInput = args.DoodlerInput;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick()
        {
            CheckInput();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void FixedTick(float deltaTime)
        {
            Move(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        private void CheckInput()
        {
            _direction = _doodlerInput.MoveDirection;
        }

        private void Move(float deltaTime)
        {
            if (_movementVelocity < _rigidbody.velocity.x)
                return;

            _rigidbody.AddForce(_movementVelocity * deltaTime * _direction, UnityEngine.ForceMode2D.Impulse);
        }
    }
}
