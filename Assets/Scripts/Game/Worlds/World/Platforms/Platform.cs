using System;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal abstract class Platform : MonoBehaviour, IPlatform
    {
        private float _xMin;
        private float _xMax;
        private float _yMin;
        private float _yMax;

        private readonly float _half = 0.5f;

        public abstract int Id { get; }

        public abstract Vector2 Size { get; }

        public Vector3 Position => transform.position;

        public abstract event Action<IPlatform> Collided;

        public abstract event Action<IPlatform> Destroyed;

        public void Init(Vector3 position)
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

        public void Clear()
        {
            gameObject.SetActive(false);
        }

        public abstract void Destroy();

#if UNITY_EDITOR
        private void OnDrawGizmos()
        {
            Gizmos.color = Color.cyan;
            Gizmos.DrawWireCube(Position, Size);
        }
#endif
    }
}
