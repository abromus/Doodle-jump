using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    public interface IDoodlerConfig : IConfig
    {
        public float MovementVelocity { get; }

        public Worlds.Entities.ShootingMode ShootingMode { get; }

        public float MaxAngle { get; }

#if UNITY_EDITOR
        public void SetMovementVelocity(float movementVelocity);

        public void SetShootingMode(Worlds.Entities.ShootingMode shootingMode);

        public void SetMaxAngle(float maxAngle);
#endif
    }
}
