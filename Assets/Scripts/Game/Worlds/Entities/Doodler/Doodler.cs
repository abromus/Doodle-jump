using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class Doodler : MonoBehaviour, IDoodler
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        //[SerializeField] private Animator _doodlerAnimator;
        [SerializeField] private BoxCollider2D _collider;

        private IUpdater _updater;
        private IDoodlerInput _doodlerInput;
        private ICameraService _cameraService;
        private IDoodlerMovement _movement;
        private IDoodlerCameraFollower _cameraFollower;
        //private IDoodlerAnimator _animator;

        public GameObject GameObject => gameObject;

        public void Init(DoodlerArgs args)
        {
            _updater = args.Updater;
            _cameraService = args.CameraService;

            _cameraService.AttachTo(transform.parent);

            InitCamera(args.CameraConfig);
            InitServices(args);
            Subscribe();
        }

        public void Jump(float height)
        {
            _movement.Jump(height);
        }

        public void Tick(float deltaTime)
        {
            _doodlerInput.Tick(deltaTime);
            _movement.Tick(deltaTime);
        }

        public void FixedTick(float deltaTime)
        {
            _movement.FixedTick(deltaTime);
            //_animator.FixedTick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            _cameraFollower.LateTick(deltaTime);
        }

        public void Destroy()
        {
            _cameraService?.Detach();

            Unsubscribe();
        }

        private void InitCamera(Settings.ICameraConfig cameraConfig)
        {
            var cameraTransform = _cameraService.Camera.transform;
            cameraTransform.localScale = Vector3.one;
            cameraTransform.position = cameraConfig.Offset;
        }

        private void InitServices(DoodlerArgs args)
        {
            var inputService = args.InputService;
            var doodlerConfig = args.DoodlerConfig;

            _doodlerInput = new DoodlerInput(inputService);

            var doodlerMovementArgs = new DoodlerMovementArgs(
                transform,
                _rigidbody,
                _doodlerInput,
                doodlerConfig);

            _movement = new DoodlerMovement(in doodlerMovementArgs);
            _cameraFollower = new DoodlerCameraFollower(transform, _cameraService.Camera.transform);
            //_animator = new DoodlerAnimator(_doodlerAnimator, _movement);
        }

        private void Subscribe()
        {
            _updater.AddUpdatable(this);
            _updater.AddFixedUpdatable(this);
            _updater.AddLateUpdatable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
            _updater.RemoveFixedUpdatable(this);
            _updater.RemoveLateUpdatable(this);
        }
    }
}
