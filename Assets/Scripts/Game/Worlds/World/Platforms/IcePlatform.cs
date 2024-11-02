using DoodleJump.Core;
using DoodleJump.Core.Services;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class IcePlatform : BasePlatform, IUpdatable, IPausable
    {
        [UnityEngine.SerializeField] private float _xOffset;
        [UnityEngine.SerializeField] private float _minSpeed;
        [UnityEngine.SerializeField] private float _maxSpeed;

        private bool _initialized;
        private IUpdater _updater;
        private IPlatformCollisionInfo _info;
        private UnityEngine.Vector3 _startPosition;
        private float _direction;
        private float _speed;
        private bool _isPaused;

        private readonly float _left = -1f;
        private readonly float _right = 1f;

        public override event System.Action<IPlatformCollisionInfo> Collided;

        public override event System.Action<IPlatform> Destroyed;

        public override void Init(Data.IGameData gameData)
        {
            base.Init(gameData);

            _updater = gameData.CoreData.ServiceStorage.GetUpdater();
        }

        public override void InitPosition(UnityEngine.Vector3 position)
        {
            base.InitPosition(position);

            _startPosition = transform.position;
            _direction = GetDirection();
            _speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);

            _initialized = true;

            Subscribe();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            Move(deltaTime);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void Destroy()
        {
            Destroyed.SafeInvoke(this);
        }

        private void Awake()
        {
            _info = new JumpPlatformCollisionInfo(this);
        }

        private void OnEnable()
        {
            if (_initialized)
                Subscribe();
        }

        private void OnDisable()
        {
            if (_initialized)
                Unsubscribe();
        }

        private void OnCollisionEnter2D(UnityEngine.Collision2D collision)
        {
            if (0f < collision.relativeVelocity.y || collision.transform.TryGetComponent<Entities.IDoodler>(out var doodler) == false)
                return;

            Collided.SafeInvoke(_info);

            PlaySound(ClipType);
        }

        private float GetDirection()
        {
            var value = UnityEngine.Random.value;

            return value < Constants.Half ? _left : _right;
        }

        private void Move(float deltaTime)
        {
            if (_isPaused)
                return;

            var position = transform.position;

            position.x += _direction * _speed * deltaTime;

            if (_direction == _left && position.x < _startPosition.x - _xOffset)
                _direction = _right;
            else if (_direction == _right && _startPosition.x + _xOffset < position.x)
                _direction = _left;

            transform.position = position;
        }

        private void Subscribe()
        {
            _updater.AddUpdatable(this);
            _updater.AddPausable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemoveUpdatable(this);
            _updater.RemovePausable(this);
        }
    }
}
