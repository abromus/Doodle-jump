using DoodleJump.Core;
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
        [SerializeField] private Transform _boosterContainer;

        private IUpdater _updater;
        private IDoodlerInput _doodlerInput;
        private ICameraService _cameraService;
        private IDoodlerMovement _movement;
        private IDoodlerShooting _shooting;
        private IDoodlerAnimator _animator;
        private IDoodlerBoosterStorage _boosterStorage;

        public GameObject GameObject => gameObject;

        public Vector2 Size => _size;

        public event System.Action Jumped;

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

        public void AddBooster(Worlds.Boosters.IBoosterCollisionInfo info)
        {
            _boosterStorage.Add(info);
        }

        public bool HasBooster(Worlds.Boosters.BoosterType boosterType)
        {
            return _boosterStorage.Has(boosterType);
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

            _shooting.Restart();
        }

        public void Destroy()
        {
            Unsubscribe();

            _boosterStorage.Destroy();
            _shooting.Destroy();
        }

        private void InitServices(DoodlerArgs args)
        {
            var inputService = args.InputService;
            var doodlerConfig = args.DoodlerConfig;
            var camera = _cameraService.Camera;
            var playerData = args.PlayerData;

            _doodlerInput = new DoodlerInput(inputService);

            var doodlerMovementArgs = new DoodlerMovementArgs(transform, _rigidbody, _doodlerInput, doodlerConfig);
            var doodlerShootingArgs = new DoodlerShootingArgs(transform, _doodlerInput, args.AudioService, args.CameraService, args.Updater, playerData, doodlerConfig, _projectilePrefab);

            _movement = new DoodlerMovement(in doodlerMovementArgs);
            _shooting = new DoodlerShooting(in doodlerShootingArgs);
            _animator = new DoodlerAnimator(transform, _doodlerAnimator, _movement, _doodlerInput);

            var doodlerBoosterStorageArgs = new DoodlerBoosterStorageArgs(_updater, args.BoosterFactory, playerData, args.BoostersConfig, _boosterContainer, this, _rigidbody);
            _boosterStorage = new DoodlerBoosterStorage(in doodlerBoosterStorageArgs);
        }

        private void Subscribe()
        {
            _updater.AddUpdatable(this);
            _updater.AddFixedUpdatable(this);
            _updater.AddPausable(this);

            _movement.Jumped += OnJumped;
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
            _updater.RemoveFixedUpdatable(this);
            _updater.RemovePausable(this);

            _movement.Jumped -= OnJumped;
        }

        private void OnJumped()
        {
            Jumped.SafeInvoke();
        }
    }
}
