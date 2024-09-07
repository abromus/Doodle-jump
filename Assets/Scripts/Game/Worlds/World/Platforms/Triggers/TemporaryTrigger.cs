using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds.Platforms
{
    internal struct TemporaryTrigger : IPlatformTrigger
    {
        private float _existenceTime;
        private readonly IDoodler _doodler;
        private readonly float _jumpForce;

        public readonly PlatformTriggerType TriggerType => PlatformTriggerType.Jump;

        public TemporaryTrigger(IPlatformCollisionInfo info, IDoodler doodler, float jumpForce)
        {
            _doodler = doodler;
            _jumpForce = jumpForce;
            _existenceTime = (info as TemporaryPlatformCollisionInfo).ExistenceTime;
        }

        public readonly void Execute()
        {
            _doodler.Jump(_jumpForce);
        }

        public void UpdateInfo(IPlatformCollisionInfo info)
        {
            if (info is TemporaryPlatformCollisionInfo springInfo)
                _existenceTime = springInfo.ExistenceTime;
        }
    }
}
