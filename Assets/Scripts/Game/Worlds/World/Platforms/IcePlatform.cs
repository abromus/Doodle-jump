using System;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Worlds.Entities;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal sealed class IcePlatform : Platform, IUpdatable, IPausable
    {
        [SerializeField] private float _xOffset;
        [SerializeField] private float _minSpeed;
        [SerializeField] private float _maxSpeed;

        private bool _initialized;
        private IUpdater _updater;
        private IPlatformCollisionInfo _info;
        private Vector3 _startPosition;
        private float _direction;
        private float _speed;
        private bool _isPaused;

        private readonly float _left = -1f;
        private readonly float _right = 1f;

        public override event Action<IPlatformCollisionInfo> Collided;

        public override event Action<IPlatform> Destroyed;

        public override void Init(IGameData gameData)
        {
            base.Init(gameData);

            _updater = gameData.CoreData.ServiceStorage.GetUpdater();
        }

        public override void InitPosition(Vector3 position)
        {
            base.InitPosition(position);

            _startPosition = transform.position;
            _direction = GetDirection();
            _speed = UnityEngine.Random.Range(_minSpeed, _maxSpeed);

            _initialized = true;

            Subscribe();
        }

        public void Tick(float deltaTime)
        {
            Move(deltaTime);
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

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

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (0f < collision.relativeVelocity.y || collision.transform.TryGetComponent<IDoodler>(out var doodler) == false)
                return;

            Collided.SafeInvoke(_info);

            PlaySound(СlipType);
        }

        private float GetDirection()
        {
            var value = UnityEngine.Random.value;
            var half = 0.5f;

            return value < half ? _left : _right;
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
