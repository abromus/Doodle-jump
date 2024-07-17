using DoodleJump.Core.Services;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal sealed class DoodlerInput : IDoodlerInput
    {
        private Vector2 _direction;

        private readonly IInputService _inputService;

        public Vector2 Direction => _direction;

        internal DoodlerInput(IInputService inputService)
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
