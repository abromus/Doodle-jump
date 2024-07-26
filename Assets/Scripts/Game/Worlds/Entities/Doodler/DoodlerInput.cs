using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerInput : IDoodlerInput
    {
        private Vector2 _direction;
        private bool _isPaused;

        private readonly IInputService _inputService;
        private readonly Vector2 _zero = Vector2.zero;

        public Vector2 Direction => _direction;

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
            _direction = _isPaused ? _zero : new Vector2(_inputService.GetHorizontalAxisRaw() * _inputService.XSensitivity, 0f);
        }
    }
}
