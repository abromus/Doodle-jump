using System.Collections.Generic;
using DoodleJump.Game.Settings;
using UnityEngine;

namespace DoodleJump.Game.Entities
{
    public readonly struct DoodlerMovementArgs
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly IDoodlerInput _doodlerInput;
        private readonly IDoodlerConfig _doodlerConfig;

        public Transform Transform => _transform;

        public Rigidbody2D Rigidbody => _rigidbody;

        public IDoodlerInput DoodlerInput => _doodlerInput;

        public IDoodlerConfig DoodlerConfig => _doodlerConfig;

        public DoodlerMovementArgs(
            Transform transform,
            Rigidbody2D rigidbody,
            IDoodlerInput doodlerInput,
            IDoodlerConfig doodlerConfig)
        {
            _transform = transform;
            _rigidbody = rigidbody;
            _doodlerInput = doodlerInput;
            _doodlerConfig = doodlerConfig;
        }
    }
}
