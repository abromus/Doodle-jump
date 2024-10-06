using DoodleJump.Core.Settings;

namespace DoodleJump.Game.Settings
{
    internal interface IDoodlerConfig : IConfig
    {
        public float MovementVelocity { get; }

        public int MaxShots { get; }

        public Worlds.Entities.ShootingMode ShootingMode { get; }

        public float MaxAngle { get; }

#if UNITY_EDITOR
        public void SetMovementVelocity(float movementVelocity);

        public void SetMaxShots(int maxShots);

        public void SetShootingMode(Worlds.Entities.ShootingMode shootingMode);

        public void SetMaxAngle(float maxAngle);
#endif
    }
}
