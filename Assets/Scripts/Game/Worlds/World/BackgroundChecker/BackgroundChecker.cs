using System.Collections.Generic;
using UnityEngine;

namespace DoodleJump.Game.Worlds
{
    internal sealed class BackgroundChecker : IBackgroundChecker
    {
        private float _highestYPosition;

        private readonly Transform _cameraTransform;
        private readonly Rect _screenRect;
        private readonly SpriteRenderer[] _backgrounds;
        private readonly Queue<SpriteRenderer> _backgroundsQueue;
        private readonly float _height;

        internal BackgroundChecker(Transform cameraTransform, Rect screenRect, SpriteRenderer[] backgrounds)
        {
            _cameraTransform = cameraTransform;
            _screenRect = screenRect;
            _backgrounds = backgrounds;
            _backgroundsQueue = new(backgrounds);
            _height = _screenRect.height;

            InitBackgrounds();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Restart()
        {
            ResetPositions();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick()
        {
            CheckDoodlerPosition();
        }

        private void InitBackgrounds()
        {
            UpdateSize();
            ResetPositions();
        }

        private void UpdateSize()
        {
            var size = _screenRect.size;

            foreach (var background in _backgrounds)
                background.size = size;
        }

        private void ResetPositions()
        {
            var previousPositionY = 0f;
            _backgroundsQueue.Clear();

            for (int i = 0; i < _backgrounds.Length; i++)
            {
                var background = _backgrounds[i];
                var transform = background.transform;
                var position = transform.position;
                position.y = i == 0 ? -_height * 2f : previousPositionY + _height;
                previousPositionY = position.y;
                transform.position = position;

                _backgroundsQueue.Enqueue(background);
            }

            _highestYPosition = _backgrounds[_backgrounds.Length - 1].transform.position.y;
        }

        private void CheckDoodlerPosition()
        {
            if (_cameraTransform.position.y + _height - 1 < _highestYPosition)
                return;

            UpdateTopPosition();
        }

        private void UpdateTopPosition()
        {
            var current = _backgroundsQueue.Dequeue();
            var transform = current.transform;
            var position = transform.position;
            position.y = _highestYPosition + _height;
            transform.position = position;

            _highestYPosition = position.y;
            _backgroundsQueue.Enqueue(current);
        }
    }
}
