﻿using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds
{
    internal struct SpringJumpTrigger : ITrigger
    {
        private bool _isSpringCollided;
        private readonly IDoodler _doodler;
        private readonly float _jumpForce;
        private readonly float _springJumpForce;

        public readonly TriggerType TriggerType => TriggerType.Jump;

        public SpringJumpTrigger(IPlatformCollisionInfo info, IDoodler doodler, float jumpForce, float springJumpForce)
        {
            _doodler = doodler;
            _jumpForce = jumpForce;
            _springJumpForce = springJumpForce;
            _isSpringCollided = (info as SpringJumpPlatformCollisionInfo).IsSpringCollided;
        }

        public readonly void Execute()
        {
            _doodler.Jump(_isSpringCollided ? _springJumpForce : _jumpForce);
        }

        public void UpdateInfo(IPlatformCollisionInfo info)
        {
            if (info is SpringJumpPlatformCollisionInfo springInfo)
                _isSpringCollided = springInfo.IsSpringCollided;
        }
    }
}