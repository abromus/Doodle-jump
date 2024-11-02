namespace DoodleJump.Game.Worlds.Platforms
{
    internal struct SpringJumpTrigger : IPlatformTrigger
    {
        private bool _isSpringCollided;
        private float _jumpForce;
        private float _springJumpForce;

        private readonly Entities.IDoodler _doodler;

        public readonly PlatformTriggerType TriggerType => PlatformTriggerType.Jump;

        internal SpringJumpTrigger(IPlatformCollisionInfo info, Entities.IDoodler doodler, float jumpForce, float springJumpForce)
        {
            _doodler = doodler;
            _jumpForce = jumpForce;
            _springJumpForce = springJumpForce;
            _isSpringCollided = (info as SpringJumpPlatformCollisionInfo).IsSpringCollided;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public readonly void Execute()
        {
            _doodler.Jump(_isSpringCollided ? _springJumpForce : _jumpForce);
        }

        public void UpdateInfo(IPlatformCollisionInfo info, Settings.IPlatformConfig config)
        {
            if (info is SpringJumpPlatformCollisionInfo springInfo)
                _isSpringCollided = springInfo.IsSpringCollided;

            if (config is Settings.ISpringJumpConfig springJumpConfig)
            {
                _jumpForce = springJumpConfig.JumpForce;
                _springJumpForce = springJumpConfig.SpringJumpForce;
            }
        }
    }
}
