using System;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class FlyingSaucer : Enemy
    {
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private Rect _screenRect;
        private IEnemyCollisionInfo _info;
        private float _direction;
        private float _speed;
        private bool _isPaused;

        public override event Action<IEnemyCollisionInfo> Collided;

        public override event Action<IEnemy> Destroyed;

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

        public override void Destroy()
        {
            base.Destroy();

            Destroyed.SafeInvoke(this);
        }

        private void Awake()
        {
            _info = new BirdCollisionInfo(this);
        }

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent<IDoodler>(out var doodler) == false)
                return;

            Collided.SafeInvoke(_info);

            PlayTriggerSound();
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent<IDoodler>(out var doodler) == false)
                return;

            Collided.SafeInvoke(_info);

            PlayTriggerSound();
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

            if (_direction == Constants.Left && position.x < _screenRect.xMin)
            {
                _direction = Constants.Right;

                SetLocalScale(_direction);
            }
            else if (_direction == Constants.Right && _screenRect.xMax < position.x)
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
