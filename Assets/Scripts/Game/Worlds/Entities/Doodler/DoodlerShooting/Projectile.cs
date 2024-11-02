using System;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class Projectile : MonoBehaviour, IProjectile, IUpdatable, IPausable
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _existenceTime;
        [SerializeField] private Vector2 _offset;
        [SerializeField] private CircleCollider2D _circleCollider;
        [SerializeField] private ProjectileClipType _projectileMovingClipType;
        [SerializeField] private ProjectileClipType _projectileDestroyedClipType;

        private IAudioService _audioService;
        private IUpdater _updater;
        private IShootingStrategy _shootingStrategy;
        private bool _initialized;
        private Vector3 _direction;
        private float _movingTime;
        private bool _isPaused;

        public GameObject GameObject => gameObject;

        public event Action<IProjectile> Destroyed;

        public void Init(IAudioService audioService, IUpdater updater,IShootingStrategy shootingStrategy)
        {
            _audioService = audioService;
            _updater = updater;
            _shootingStrategy = shootingStrategy;
        }

        public void InitPosition(Vector3 doodlerPosition, float doodlerDirection, Vector3 shootPosition)
        {
            doodlerPosition.x += doodlerDirection * _offset.x;
            doodlerPosition.y += _offset.y;
            transform.position = doodlerPosition;
            gameObject.SetActive(true);

            _direction = _shootingStrategy.GetDirection(doodlerPosition, shootPosition);
            _movingTime = 0f;
            _audioService.PlaySound(_projectileMovingClipType);

            Subscribe();

            _initialized = true;
        }

        public void Tick(float deltaTime)
        {
            if (_initialized == false || _isPaused)
                return;

            _movingTime += deltaTime;

            if (_movingTime < _existenceTime)
                transform.position += _speed * deltaTime * _direction;
            else
                Destroyed.SafeInvoke(this);
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        public void Clear()
        {
            Destroy();

            _audioService.PlaySound(_projectileDestroyedClipType);
        }

        public void Destroy()
        {
            Unsubscribe();

            gameObject.SetActive(false);

            _movingTime = 0f;
            _initialized = false;
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireSphere(transform.position, _circleCollider.radius);
        }
#endif

        private void OnCollisionEnter2D(Collision2D collision)
        {
            if (collision.transform.TryGetComponent<IEnemy>(out var enemy) == false)
                return;

            Destroyed.SafeInvoke(this);
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.transform.TryGetComponent<IEnemy>(out var enemy) == false)
                return;

            Destroyed.SafeInvoke(this);
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
