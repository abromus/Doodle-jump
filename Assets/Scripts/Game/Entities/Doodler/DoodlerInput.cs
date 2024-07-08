using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Entities
{
    internal sealed class DoodlerInput : IDoodlerInput
    {
        private Vector2 _direction;

        private readonly IInputService _inputService;

        public Vector2 Direction => _direction;

        public DoodlerInput(IInputService inputService)
        {
            _inputService = inputService;
        }

        public void Tick(float deltaTime)
        {
            CheckInput();
        }

        private void CheckInput()
        {
            _direction = new Vector2(_inputService.GetHorizontalAxisRaw(), 0f).normalized;
        }
    }
}
