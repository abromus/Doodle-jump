using DoodleJump.Core;
using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class Doodler : MonoBehaviour, IDoodler
    {
        [SerializeField] private Rigidbody2D _rigidbody;
        [SerializeField] private Animator _doodlerAnimator;
        [SerializeField] private Vector2 _size;
        [SerializeField] private Projectile _projectilePrefab;
        [SerializeField] private Transform _boosterContainer;
        [Separator(CustomColor.Lime)]
        [SerializeField] private float _parabolaMoveDuration;
        [SerializeField] private float _parabolaHeight;
        [SerializeField] private float _downMoveDuration;
        [Separator(CustomColor.MediumTurquoise)]
        [SerializeField] private GameObject _head;
        [SerializeField] private GameObject _shootingHead;
        [SerializeField] private DoodlerNose _nose;

        private IUpdater _updater;
        private IDoodlerInput _doodlerInput;
        private IDoodlerMovement _movement;
        private IDoodlerShooting _shooting;
        private IDoodlerAnimator _animator;
        private IDoodlerBoosterStorage _boosterStorage;

        private bool _isUpdatable;

        public GameObject GameObject => gameObject;

        public Vector2 Size => _size;

        public event System.Action Jumped;

        public void Init(in DoodlerArgs args)
        {
            _updater = args.Updater;

            InitServices(in args);
            Subscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Jump(float height)
        {
            _movement.Jump(height);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void AddBooster(Worlds.Boosters.IBoosterCollisionInfo info)
        {
            _boosterStorage.Add(info);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public bool HasBooster(Worlds.Boosters.BoosterType boosterType)
        {
            return _boosterStorage.Has(boosterType);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetProjectileContainer(Transform projectilesContainer)
        {
            _shooting.SetProjectileContainer(projectilesContainer);
        }

        public void GameOver(GameOverType type)
        {
            _movement.GameOver(type);

            MakeUpdatable(false);
        }

        public void Restart()
        {
            MakeUpdatable(true);

            _rigidbody.velocity = Vector3.zero;

            transform.position = Vector3.zero;

            _shooting.Restart();
            _movement.Restart();
            _animator.Restart();
        }

        public void Tick(float deltaTime)
        {
            _doodlerInput.Tick(deltaTime);
            _movement.Tick(deltaTime);
            _shooting.Tick(deltaTime);
            _animator.Tick(deltaTime);
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

        public void Destroy()
        {
            Unsubscribe();

            _boosterStorage.Destroy();
            _shooting.Destroy();
        }

        private void InitServices(in DoodlerArgs args)
        {
            var inputService = args.InputService;
            var cameraService = args.CameraService;
            var doodlerConfig = args.DoodlerConfig;
            var playerData = args.PlayerData;

            _doodlerInput = new DoodlerInput(inputService, transform);

            var doodlerMovementAnimationArgs = new DoodlerMovementAnimationArgs(
                _parabolaMoveDuration,
                _parabolaHeight,
                _downMoveDuration);

            var doodlerMovementArgs = new DoodlerMovementArgs(
                transform,
                _rigidbody,
                _doodlerInput,
                cameraService,
                doodlerConfig,
                in doodlerMovementAnimationArgs);

            var doodlerShootingArgs = new DoodlerShootingArgs(
                transform,
                _doodlerInput,
                args.AudioService,
                cameraService,
                args.Updater,
                playerData,
                doodlerConfig,
                _projectilePrefab,
                _nose);

            var doodlerBoosterStorageArgs = new DoodlerBoosterStorageArgs(
                _updater,
                args.BoosterFactory,
                playerData,
                args.BoostersConfig,
                _boosterContainer,
                this,
                _rigidbody);

            _nose.Init();

            _movement = new DoodlerMovement(in doodlerMovementArgs);
            _shooting = new DoodlerShooting(in doodlerShootingArgs);
            _boosterStorage = new DoodlerBoosterStorage(in doodlerBoosterStorageArgs);

            var doodlerAnimatorArgs = new DoodlerAnimatorArgs(
                transform,
                _doodlerAnimator,
                _head,
                _shootingHead,
                _doodlerInput,
                _movement,
                playerData,
                doodlerConfig);

            _animator = new DoodlerAnimator(in doodlerAnimatorArgs);
        }

        private void MakeUpdatable(bool isUpdatable)
        {
            if (isUpdatable)
            {
                if (_isUpdatable)
                    return;

                _updater.AddUpdatable(this);

                _isUpdatable = true;
            }
            else
            {
                _updater.RemoveUpdatable(this);

                _isUpdatable = false;
            }
        }

        private void Subscribe()
        {
            _updater.AddFixedUpdatable(this);
            _updater.AddPausable(this);

            _movement.Jumped += OnJumped;
        }

        private void Unsubscribe()
        {
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
