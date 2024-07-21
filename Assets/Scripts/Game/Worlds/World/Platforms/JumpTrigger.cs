using DoodleJump.Game.Worlds.Entities;

namespace DoodleJump.Game.Worlds
{
    internal readonly struct JumpTrigger : ITrigger
    {
        private readonly IDoodler _doodler;
        private readonly float _jumpForce;

        public readonly TriggerType TriggerType => TriggerType.Jump;

        public JumpTrigger(IDoodler doodler, float jumpForce)
        {
            _doodler = doodler;
            _jumpForce = jumpForce;
        }

        public readonly void Execute()
        {
            _doodler.Jump(_jumpForce);
        }
    }
}
