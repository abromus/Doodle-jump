using System;
using DoodleJump.Core;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class Bird : Enemy
    {
        [SerializeField] private float _xOffset;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private IEnemyCollisionInfo _info;
        private Vector3 _startPosition;
        private float _direction;
        private float _speed;
        private bool _isPaused;

        public override event Action<IEnemyCollisionInfo> Collided;

        public override event Action<IEnemy> Destroyed;

        public override void InitPosition(Vector3 position)
        {
            base.InitPosition(position);

            _startPosition = transform.position;
            _direction = GetDirection();
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
            var transform = collision.transform;

            if (transform.TryGetComponent<IDoodler>(out var doodler))
            {
                Collided.SafeInvoke(_info);

                PlayTriggerSound();
            }
            else if (transform.TryGetComponent<IProjectile>(out var projectile))
            {
                Destroyed.SafeInvoke(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var transform = collision.transform;

            if (transform.TryGetComponent<IDoodler>(out var doodler))
            {
                Collided.SafeInvoke(_info);

                PlayTriggerSound();
            }
            else if (transform.TryGetComponent<IProjectile>(out var projectile))
            {
                Destroyed.SafeInvoke(this);
            }
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

            if (_direction == Constants.Left && position.x < _startPosition.x - _xOffset)
            {
                _direction = Constants.Right;

                SetLocalScale(_direction);
            }
            else if (_direction == Constants.Right && _startPosition.x + _xOffset < position.x)
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
