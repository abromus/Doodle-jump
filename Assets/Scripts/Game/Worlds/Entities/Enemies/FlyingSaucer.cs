using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class FlyingSaucer : Enemy
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private AnimationCurve _animationCurve;
        [SerializeField] private float _offsetY;

        private Rect _screenRect;
        private IEnemyCollisionInfo _info;
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

            var size = transform.localScale.x;
            position.x = _direction == Constants.Right ? _screenRect.xMin - size : _screenRect.xMax + size;

            base.InitPosition(position);

            _speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);
            _defaultY = position.y;

            SetLocalScale(_direction);
        }

        public override void Tick(float deltaTime)
        {
            Move(deltaTime);
        }

        public override void SetPause(bool isPaused)
        {
            base.SetPause(isPaused);

            _isPaused = isPaused;
        }

        protected override IEnemyCollisionInfo GetCollisionInfo()
        {
            return _info;
        }

        private void Awake()
        {
            _info = new BirdCollisionInfo(this);
        }

        private float GetDirection()
        {
            var value = UnityEngine.Random.value;
            var half = 0.5f;

            return value < half ? Constants.Left : Constants.Right;
        }

        private void Move(float deltaTime)
        {
            if (_isPaused)
                return;

            var position = transform.position;
            position.x += _direction * _speed * deltaTime;

            var isLeft = _direction == Constants.Left;
            var progress = isLeft ? (position.x - _screenRect.xMin) / _screenRect.width : (position.x + _screenRect.xMax) / _screenRect.width;
            position.y = _defaultY + _direction * _animationCurve.Evaluate(progress) * _offsetY;

            if (isLeft && position.x < _screenRect.xMin)
            {
                _direction = Constants.Right;

                SetLocalScale(_direction);
            }
            else if (isLeft == false && _screenRect.xMax < position.x)
            {
                _direction = Constants.Left;

                SetLocalScale(_direction);
            }

            transform.position = position;
        }

        private void SetLocalScale(float scale)
        {
            var localScale = transform.localScale;
            localScale.x = scale;
            transform.localScale = localScale;
        }
    }
}
