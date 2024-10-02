using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal readonly struct JumpTrigger : IPlatformTrigger
    {
        private readonly IDoodler _doodler;
        private readonly float _jumpForce;

        public readonly PlatformTriggerType TriggerType => PlatformTriggerType.Jump;

        internal JumpTrigger(IDoodler doodler, float jumpForce)
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
        public void UpdateInfo(IPlatformCollisionInfo info) { }
    }
}
