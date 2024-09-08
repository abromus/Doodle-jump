using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerInput : IDoodlerInput
    {
        private Vector2 _moveDirection;
        private Vector2 _shootPosition;
        private bool _isShooting;
        private bool _isPaused;

        private readonly IInputService _inputService;
        private readonly Vector2 _zero = Vector2.zero;

        public Vector2 MoveDirection => _moveDirection;

        public bool IsShooting => _isShooting;

        public Vector2 ShootPosition => _shootPosition;

        internal DoodlerInput(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Tick(float deltaTime)
        {
            CheckInput();
        }

        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void CheckInput()
        {
            _moveDirection = _isPaused ? _zero : new Vector2(_inputService.GetHorizontalAxisRaw() * _inputService.XSensitivity, 0f);

#if UNITY_ANDROID
            var touchCount = _inputService.GetTouchCount();
            var hasTouch = touchCount == 1;

            if (hasTouch)
            {
                var firstTouchIndex = 0;
                var touch = _inputService.GetTouch(firstTouchIndex);

                _isShooting = hasTouch && touch.phase == TouchPhase.Began;
                _shootPosition = _isShooting ? touch.position : _zero;
            }
            else
            {
                _isShooting = false;
                _shootPosition = _zero;
            }
#else
            _isShooting = _inputService.GetMouseButtonDown(0);
            _shootPosition = _isShooting ? _inputService.GetMousePosition() : _zero;
#endif

        }
    }
}
