﻿using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal readonly struct TemporaryTrigger : IPlatformTrigger
    {
        private readonly IDoodler _doodler;
        private readonly float _jumpForce;

        public readonly PlatformTriggerType TriggerType => PlatformTriggerType.Jump;

        public TemporaryTrigger(IDoodler doodler, float jumpForce)
        {
            _doodler = doodler;
            _jumpForce = jumpForce;
        }

        public readonly void Execute()
        {
            _doodler.Jump(_jumpForce);
        }

        public readonly void UpdateInfo(IPlatformCollisionInfo info) { }
    }
}