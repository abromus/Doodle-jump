using System;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal abstract class Enemy : MonoBehaviour, IEnemy
    {
        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;
        private IAudioService _audioService;
        private AudioSource _loopSound;

        private readonly float _half = 0.5f;

        public abstract int Id { get; }

        public abstract Vector2 Size { get; }

        public Vector3 Position => transform.position;

        public abstract event Action<IEnemyCollisionInfo> Collided;

        public abstract event Action<IEnemy> Destroyed;

        public virtual void Init(IGameData gameData)
        {
            _audioService = gameData.ServiceStorage.GetAudioService();
        }

        public virtual void InitPosition(Vector3 position)
        {
            transform.position = position;

            var xCenter = position.x;
            var yCenter = position.y;
            var xOffset = Size.x * _half;
            var yOffset = Size.y * _half;

            _xMin = xCenter - xOffset;
            _xMax = xCenter + xOffset;
            _yMin = yCenter - yOffset;
            _yMax = yCenter + yOffset;

            gameObject.SetActive(true);
        }

        public bool IsIntersectedArea(Vector2 center, Vector2 size)
        {
            var xCenter = center.x;
            var yCenter = center.y;
            var xOffset = size.x * _half;
            var yOffset = size.y * _half;

            var xMin = Mathf.Max(_xMin, xCenter - xOffset);
            var xMax = Mathf.Min(_xMax, xCenter + xOffset);
            var yMin = Mathf.Max(_yMin, yCenter - yOffset);
            var yMax = Mathf.Min(_yMax, yCenter + yOffset);

            return xMin <= xMax && yMin <= yMax;
        }

        public abstract void Tick(float deltaTime);

        public abstract void SetPause(bool isPaused);

        public void Clear()
        {
            StopLoopSound();

            gameObject.SetActive(false);
        }

        public virtual void Destroy()
        {
            StopLoopSound();
        }

        protected void PlaySound(EnemyClipType type)
        {
            StopLoopSound();

            _loopSound = _audioService.PlayLoopSound(type);
        }

        protected void PlaySound(EnemyTriggerClipType type)
        {
            _audioService.PlaySound(type);
        }

        private void OnEnable()
        {
            _loopSound.Play();
        }

        private void OnDisable()
        {
            _loopSound.Pause();
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.magenta;
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
    }
}
