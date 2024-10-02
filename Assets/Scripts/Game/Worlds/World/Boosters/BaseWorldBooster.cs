using System;
using DoodleJump.Core.Services;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Boosters
{
    internal abstract class BaseWorldBooster : MonoBehaviour, IWorldBooster
    {
        [SerializeField] private int _id;
        [SerializeField] private Vector2 _size;
        [SerializeField] private BoosterType _boosterType;
        [SerializeField] private BoosterClipType _clipType;
        [SerializeField] private Animator _animator;

        private IAudioService _audioService;
        private IUpdater _updater;
        private AudioSource _loopSound;
        private Transform _defaultParent;
        private bool _initialized;

        public int Id => _id;

        public Vector2 Size => _size;

        public BoosterType BoosterType => _boosterType;

        public GameObject GameObject => gameObject;

        public Vector3 Position => transform.position;

        public abstract IBoosterCollisionInfo Info { get; }

        protected BoosterClipType ClipType => _clipType;

        public abstract event Action<IBoosterCollisionInfo> Collided;

        public abstract event Action<IWorldBooster> Destroyed;

        public virtual void Init(IGameData gameData)
        {
            _audioService = gameData.ServiceStorage.GetAudioService();
            _updater = gameData.CoreData.ServiceStorage.GetUpdater();

            _initialized = true;

            Subscribe();
        }

        public virtual void InitPosition(Vector3 position)
        {
            transform.position = position;
            gameObject.SetActive(true);

            PlaySound(_clipType);
        }

        public virtual void Tick(float deltaTime) { }

        public virtual void SetPause(bool isPaused)
        {
            _animator.speed = isPaused ? Constants.PauseSpeed : Constants.ActiveSpeed;
        }

        public void Clear()
        {
            StopLoopSound();

            transform.SetParent(_defaultParent);
            gameObject.SetActive(false);
        }

        public virtual void Destroy()
        {
            StopLoopSound();
        }

        protected void PlaySound(BoosterClipType type)
        {
            StopLoopSound();

            _loopSound = _audioService.PlayLoopSound(type);
        }

        private void Awake()
        {
            _defaultParent = transform.parent;
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

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Position, Size);
        }
#endif

        private void StopLoopSound()
        {
            if (_loopSound == null)
                return;

            _audioService.StopLoopSound(_loopSound);
            _loopSound = null;
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
