using System;
using DoodleJump.Core;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal abstract class BaseEnemy : MonoBehaviour, IEnemy
    {
        [SerializeField] private int _id;
        [SerializeField] private Vector2 _size;
        [SerializeField] private float _repulsiveForce;
        [SerializeField] private EnemyClipType _clipType;
        [SerializeField] private EnemyTriggerClipType _triggerClipType;
        [SerializeField] private Worlds.Boosters.ShotWorldBooster _shotWorldBoosterPrefab;
        [SerializeField] private Transform _shotWorldBoosterContainer;
        [SerializeField] private Animator _animator;

        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;
        private IGameData _gameData;
        private IAudioService _audioService;
        private IUpdater _updater;
        private AudioSource _loopSound;
        private bool _initialized;
        private Worlds.Boosters.IWorldBooster _shotWorldBooster;

        public int Id => _id;

        public Vector2 Size => _size;

        public Vector3 Position => transform.position;

        public event Action<IEnemyCollisionInfo> Collided;

        public event Action<Worlds.Boosters.IWorldBooster, Worlds.Boosters.BoosterTriggerType> BoosterDropped;

        public event Action<IEnemy> Destroyed;

        public virtual void Init(IGameData gameData)
        {
            _gameData = gameData;
            _audioService = _gameData.ServiceStorage.GetAudioService();
            _updater = _gameData.CoreData.ServiceStorage.GetUpdater();

            CreateBooster();

            _initialized = true;

            Subscribe();
        }

        public virtual void InitPosition(Vector3 position)
        {
            transform.position = position;

            var xCenter = position.x;
            var yCenter = position.y;
            var xOffset = Size.x * Constants.Half;
            var yOffset = Size.y * Constants.Half;

            _xMin = xCenter - xOffset;
            _xMax = xCenter + xOffset;
            _yMin = yCenter - yOffset;
            _yMax = yCenter + yOffset;

            gameObject.SetActive(true);

            PlaySound(_clipType);
        }

        public bool IsIntersectedArea(Vector2 center, Vector2 size)
        {
            var xCenter = center.x;
            var yCenter = center.y;
            var xOffset = size.x * Constants.Half;
            var yOffset = size.y * Constants.Half;

            var xMin = Mathf.Max(_xMin, xCenter - xOffset);
            var xMax = Mathf.Min(_xMax, xCenter + xOffset);
            var yMin = Mathf.Max(_yMin, yCenter - yOffset);
            var yMax = Mathf.Min(_yMax, yCenter + yOffset);

            return xMin <= xMax && yMin <= yMax;
        }

        public virtual void FixedTick(float deltaTime) { }

        public virtual void SetPause(bool isPaused)
        {
            _animator.speed = isPaused ? Constants.PauseSpeed : Constants.ActiveSpeed;
        }

        public void Clear()
        {
            StopLoopSound();

            gameObject.SetActive(false);
        }

        public void Destroy()
        {
            StopLoopSound();

            Destroyed.SafeInvoke(this);
        }

        protected abstract IEnemyCollisionInfo GetCollisionInfo();

        protected void PlaySound(EnemyClipType type)
        {
            StopLoopSound();

            _loopSound = _audioService.PlayLoopSound(type);
        }

        protected void PlayTriggerSound()
        {
            _audioService.PlaySound(_triggerClipType);
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
            var transform = collision.transform;

            if (transform.TryGetComponent<IDoodler>(out var doodler))
            {
                if (collision.relativeVelocity.y < 0f)
                {
                    doodler.Jump(_repulsiveForce);

                    if (_shotWorldBooster != null)
                        BoosterDropped.SafeInvoke(_shotWorldBooster, Worlds.Boosters.BoosterTriggerType.Shots);

                    Destroyed.SafeInvoke(this);
                }
                else if (doodler.HasBooster(Worlds.Boosters.BoosterType.Shield) == false && doodler.HasBooster(Worlds.Boosters.BoosterType.Jetpack) == false)
                {
                    var collisionInfo = GetCollisionInfo();

                    Collided.SafeInvoke(collisionInfo);

                    PlayTriggerSound();
                }
            }
            else if (transform.TryGetComponent<IProjectile>(out var projectile))
            {
                Destroyed.SafeInvoke(this);
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            var transform = collision.transform;

            if (transform.TryGetComponent<IDoodler>(out var doodler) && doodler.HasBooster(Worlds.Boosters.BoosterType.Shield) == false && doodler.HasBooster(Worlds.Boosters.BoosterType.Jetpack) == false)
            {
                var collisionInfo = GetCollisionInfo();

                Collided.SafeInvoke(collisionInfo);

                PlayTriggerSound();
            }
            else if (transform.TryGetComponent<IProjectile>(out var projectile))
            {
                Destroyed.SafeInvoke(this);
            }
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
            Gizmos.DrawWireCube(Position, Size);
        }
#endif

        private void CreateBooster()
        {
            if (_shotWorldBoosterPrefab == null)
                return;

            _shotWorldBooster = Instantiate(_shotWorldBoosterPrefab, _shotWorldBoosterContainer);
            _shotWorldBooster.Init(_gameData);
        }

        private void StopLoopSound()
        {
            if (_loopSound == null)
                return;

            _audioService.StopLoopSound(_loopSound);
            _loopSound = null;
        }

        private void Subscribe()
        {
            _updater.AddFixedUpdatable(this);
            _updater.AddPausable(this);
        }

        private void Unsubscribe()
        {
            _updater.RemoveFixedUpdatable(this);
            _updater.RemovePausable(this);
        }
    }
}
