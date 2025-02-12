﻿using DoodleJump.Core.Services;
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
        private readonly Transform _transform;
        private readonly Vector2 _zero = Vector2.zero;

        public Vector2 MoveDirection => _moveDirection;

        public bool IsShooting => _isShooting;

        public Vector2 ShootPosition => _shootPosition;

        internal DoodlerInput(IInputService inputService, Transform transform)
        {
            _inputService = inputService;
            _transform = transform;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void Tick(float deltaTime)
        {
            CheckInput();
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetPause(bool isPaused)
        {
            _isPaused = isPaused;
        }

        private void CheckInput()
        {
            _moveDirection = _isPaused ? _zero : new Vector2(_inputService.GetHorizontalAxisRaw() * _inputService.XSensitivity, 0f);

#if UNITY_EDITOR == false && UNITY_ANDROID
            var touchCount = _inputService.GetTouchCount();
            var hasTouch = touchCount == 1;

            if (hasTouch)
            {
                var firstTouchIndex = 0;
                var touch = _inputService.GetTouch(firstTouchIndex);

                _isShooting = touch.phase == TouchPhase.Began && _inputService.IsPointerOverGameObject(_transform.gameObject.scene, firstTouchIndex) == false;
                _shootPosition = _isShooting ? touch.position : _zero;
            }
            else
            {
                _isShooting = false;
                _shootPosition = _zero;
            }
#else
            _isShooting = _inputService.GetMouseButtonDown(0) && _inputService.IsPointerOverGameObject(_transform.gameObject.scene) == false;
            _shootPosition = _isShooting ? _inputService.GetMousePosition() : _zero;
#endif
        }
    }
}
