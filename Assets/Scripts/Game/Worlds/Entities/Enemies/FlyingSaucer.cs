using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class FlyingSaucer : BaseEnemy
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _offsetY;

        private Rect _screenRect;
        private IEnemyCollisionInfo _info;
        private float _halfXSize;
        private float _direction;
        private float _speed;
        private float _defaultY;
        private bool _isPaused;

        public override void Init(IGameData gameData)
        {
            base.Init(gameData);

            _screenRect = gameData.CoreData.ServiceStorage.GetCameraService().GetScreenRect();
        }

        public override void InitPosition(Vector3 position)
        {
            _direction = GetDirection();

            var size = Size.x * Constants.Half;
            position.x = _direction == Constants.Right ? _screenRect.xMin + size : _screenRect.xMax - size;

            base.InitPosition(position);

            _speed = Random.Range(_minSpeed, _maxSpeed);
            _defaultY = position.y;

            SetLocalScale(_direction);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public override void FixedTick(float deltaTime)
        {
            Move(deltaTime);
        }

        public override void SetPause(bool isPaused)
        {
            base.SetPause(isPaused);

            _isPaused = isPaused;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        protected override IEnemyCollisionInfo GetCollisionInfo()
        {
            return _info;
        }

        private void Awake()
        {
            _info = new BirdCollisionInfo(this);

            _halfXSize = Size.x * Constants.Half;
        }

        private float GetDirection()
        {
            var value = Random.value;

            return value < Constants.Half ? Constants.Left : Constants.Right;
        }

        private void Move(float deltaTime)
        {
            if (_isPaused)
                return;

            transform.position = GetPosition(deltaTime);

            CheckDirection(transform.position.x);
        }

        private Vector3 GetPosition(float deltaTime)
        {
            var position = transform.position;
            position.x += _direction * _speed * deltaTime;
            position.y = GetPositionY();

            return position;

            float GetPositionY()
            {
                var screenWidth = _screenRect.width;
                var newScreenWidth = screenWidth - Size.x;

                var newXMin = 0f;
                var oldMinProgress = 0f;
                var oldMaxProgress = 1f;

                var newX = Map(position.x, _screenRect.xMin, _screenRect.xMax, newXMin, newScreenWidth);
                var screenFactor = _halfXSize / screenWidth;
                var progress = Map(newX / newScreenWidth, oldMinProgress, oldMaxProgress, oldMinProgress - screenFactor, oldMaxProgress + screenFactor);
                var positionY = _defaultY + _direction * _animationCurve.Evaluate(progress) * _offsetY;

                return positionY;
            }

            [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
            float Map(float x, float oldMin, float oldMax, float newMin, float newMax)
            {
                return newMin + (x - oldMin) * (newMax - newMin) / (oldMax - oldMin);
            }
        }

        private void CheckDirection(float positionX)
        {
            var isLeft = _direction == Constants.Left;

            if (isLeft && positionX < _screenRect.xMin + _halfXSize)
            {
                _direction = Constants.Right;

                SetLocalScale(_direction);
            }
            else if (isLeft == false && _screenRect.xMax - _halfXSize < positionX)
            {
                _direction = Constants.Left;

                SetLocalScale(_direction);
            }
        }

        private void SetLocalScale(float scale)
        {
            var localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;
        }
    }
}
