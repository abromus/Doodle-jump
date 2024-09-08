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
        [SerializeField] private Projectile _projectilePrefab;

        private IUpdater _updater;
        private IDoodlerInput _doodlerInput;
        private ICameraService _cameraService;
        private IDoodlerMovement _movement;
        private IDoodlerShooting _shooting;
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

        public void SetProjectileContainer(Transform projectilesContainer)
        {
            _shooting.SetProjectileContainer(projectilesContainer);
        }

        public void Tick(float deltaTime)
        {
            _doodlerInput.Tick(deltaTime);
            _movement.Tick(deltaTime);
            _shooting.Tick(deltaTime);
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

        public void SetPause(bool isPaused)
        {
            _doodlerInput.SetPause(isPaused);
            _movement.SetPause(isPaused);
            _animator.SetPause(isPaused);
            _shooting.SetPause(isPaused);
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
            var camera = _cameraService.Camera;

            _doodlerInput = new DoodlerInput(inputService);

            var doodlerMovementArgs = new DoodlerMovementArgs(transform, _rigidbody, _doodlerInput, doodlerConfig);
            var doodlerShootingArgs = new DoodlerShootingArgs(transform, _doodlerInput, args.AudioService, args.Updater, camera, _projectilePrefab);

            _movement = new DoodlerMovement(in doodlerMovementArgs);
            _shooting = new DoodlerShooting(doodlerShootingArgs);
            _cameraFollower = new DoodlerCameraFollower(transform, camera.transform);
            _animator = new DoodlerAnimator(transform, _doodlerAnimator, _movement, _doodlerInput);
        }

        private void Subscribe()
        {
            _updater.AddUpdatable(this);
            _updater.AddFixedUpdatable(this);
            _updater.AddLateUpdatable(this);
            _updater.AddPausable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
            _updater.RemoveFixedUpdatable(this);
            _updater.RemoveLateUpdatable(this);
            _updater.RemovePausable(this);
        }
    }
}
