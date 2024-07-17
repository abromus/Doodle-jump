﻿using DoodleJump.Game.Settings;
using UnityEngine;

namespace DoodleJump.Game.Worlds.Entities
{
    internal readonly struct DoodlerMovementArgs
    {
        private readonly Transform _transform;
        private readonly Rigidbody2D _rigidbody;
        private readonly IDoodlerInput _doodlerInput;
        private readonly IDoodlerConfig _doodlerConfig;

        internal Transform Transform => _transform;

        internal Rigidbody2D Rigidbody => _rigidbody;

        internal IDoodlerInput DoodlerInput => _doodlerInput;

        internal IDoodlerConfig DoodlerConfig => _doodlerConfig;

        internal DoodlerMovementArgs(
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