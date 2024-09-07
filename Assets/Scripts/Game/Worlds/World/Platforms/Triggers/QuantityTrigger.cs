using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal readonly struct QuantityTrigger : IPlatformTrigger
    {
        private readonly IDoodler _doodler;
        private readonly float _jumpForce;

        public readonly PlatformTriggerType TriggerType => PlatformTriggerType.Jump;

        public QuantityTrigger(IDoodler doodler, float jumpForce)
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
