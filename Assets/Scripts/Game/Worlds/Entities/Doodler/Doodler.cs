using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class Doodler : MonoBehaviour, IDoodler
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _doodlerAnimator;
        [SerializeField] private BoxCollider2D _collider;
        [SerializeField] private Vector2 _size;

        private IUpdater _updater;
        private IDoodlerInput _doodlerInput;
        private ICameraService _cameraService;
        private IDoodlerMovement _movement;
        private IDoodlerCameraFollower _cameraFollower;
        private IDoodlerAnimator _animator;

        public GameObject GameObject => gameObject;

        public Vector2 Size => _size;

        public void Init(DoodlerArgs args)
        {
            _updater = args.Updater;
            _cameraService = args.CameraService;

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
            _animator.FixedTick(deltaTime);
        }

        public void LateTick(float deltaTime)
        {
            _cameraFollower.LateTick(deltaTime);
        }

        public void Restart()
        {
            _rigidbody.velocity = Vector3.zero;

            transform.position = Vector3.zero;
        }

        public void Destroy()
        {
            Unsubscribe();
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
            _animator = new DoodlerAnimator(_doodlerAnimator, _movement);
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
