namespace DoodleJump.Game.Worlds.Platforms
{
    internal struct TemporaryTrigger : IPlatformTrigger
    {
        private float _jumpForce;

        private readonly Entities.IDoodler _doodler;

        public readonly PlatformTriggerType TriggerType => PlatformTriggerType.Jump;

        internal TemporaryTrigger(Entities.IDoodler doodler, float jumpForce)
        {
            _doodler = doodler;
            _jumpForce = jumpForce;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public readonly void Execute()
        {
            _doodler.Jump(_jumpForce);
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void UpdateInfo(IPlatformCollisionInfo info, Settings.IPlatformConfig config)
        {
            if (config is not Settings.ITemporaryConfig temporaryConfig)
                return;

            _jumpForce = temporaryConfig.JumpForce;
        }
    }
}
