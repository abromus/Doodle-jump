using System;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class DoodlerChecker : IDoodlerChecker
    {
        private readonly Transform _doodlerTransform;
        private readonly Transform _cameraTransform;
        private readonly float _offset;

        private readonly float _half = 0.5f;

        public event Action GameOver;

        internal DoodlerChecker(Transform doodlerTransform, Transform cameraTransform, Rect screenRect)
        {
            _doodlerTransform = doodlerTransform;
            _cameraTransform = cameraTransform;
            _offset = screenRect.height * _half;
        }

        public void Tick()
        {
            CheckDoodlerPosition();
        }

        private void CheckDoodlerPosition()
        {
            if (_cameraTransform.position.y < _doodlerTransform.position.y + _offset)
                return;

            GameOver?.Invoke();
        }
    }
}
