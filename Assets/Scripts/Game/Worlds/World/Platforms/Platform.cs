using System;
using DoodleJump.Game.Data;
using DoodleJump.Game.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal abstract class Platform : MonoBehaviour, IPlatform
    {
        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;
        private IAudioService _audioService;

        private readonly float _half = 0.5f;

        public abstract int Id { get; }

        public abstract Vector2 Size { get; }

        public Vector3 Position => transform.position;

        public abstract event Action<IPlatformCollisionInfo> Collided;

        public abstract event Action<IPlatform> Destroyed;

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

        public virtual bool IsIntersectedArea(Vector2 center, Vector2 size)
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

        public void Clear()
        {
            gameObject.SetActive(false);
        }

        public abstract void Destroy();

        protected void PlaySound(ClipType type)
        {
            _audioService.PlaySound(type);
        }

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Position, Size);
        }
#endif
    }
}
