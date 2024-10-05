namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerMovementArgs
    {
        private readonly UnityEngine.Transform _transform;
        private readonly UnityEngine.Rigidbody2D _rigidbody;
        private readonly IDoodlerInput _doodlerInput;
        private readonly Core.Services.ICameraService _cameraService;
        private readonly Settings.IDoodlerConfig _doodlerConfig;
        private readonly DoodlerMovementAnimationArgs _movementAnimationArgs;

        internal readonly UnityEngine.Transform Transform => _transform;

        internal readonly UnityEngine.Rigidbody2D Rigidbody => _rigidbody;

        internal readonly IDoodlerInput DoodlerInput => _doodlerInput;

        internal readonly Core.Services.ICameraService CameraService => _cameraService;

        internal readonly Settings.IDoodlerConfig DoodlerConfig => _doodlerConfig;

        internal readonly DoodlerMovementAnimationArgs MovementAnimationArgs => _movementAnimationArgs;

        internal DoodlerMovementArgs(
            UnityEngine.Transform transform,
            UnityEngine.Rigidbody2D rigidbody,
            IDoodlerInput doodlerInput,
            Core.Services.ICameraService cameraService,
            Settings.IDoodlerConfig doodlerConfig,
            in DoodlerMovementAnimationArgs movementAnimationArgs)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _doodlerInput = doodlerInput;
            _cameraService = cameraService;
            _doodlerConfig = doodlerConfig;
            _movementAnimationArgs = movementAnimationArgs;
        }
    }
}
