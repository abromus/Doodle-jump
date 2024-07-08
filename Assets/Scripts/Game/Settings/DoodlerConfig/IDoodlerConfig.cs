using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    public interface IDoodlerConfig : IConfig
    {
        public float MovementVelocity { get; }
    }
}
