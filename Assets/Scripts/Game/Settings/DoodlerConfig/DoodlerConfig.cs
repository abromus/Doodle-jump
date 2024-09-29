using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(DoodlerConfig), menuName = ConfigKeys.GamePathKey + nameof(DoodlerConfig))]
    public sealed class DoodlerConfig : ScriptableObject, IDoodlerConfig
    {
        [SerializeField] private float _movementVelocity = 7.5f;
        [SerializeField] private int _maxShots = 120;
        [SerializeField] private Worlds.Entities.ShootingMode _shootingMode;
        [SerializeField] private float _maxAngle;

        public float MovementVelocity => _movementVelocity;

        public int MaxShots => _maxShots;

        public Worlds.Entities.ShootingMode ShootingMode => _shootingMode;

        public float MaxAngle => _maxAngle;

#if UNITY_EDITOR
        public void SetMovementVelocity(float movementVelocity)
        {
            _movementVelocity = movementVelocity;
        }

        public void SetMaxShots(int maxShots)
        {
            _maxShots = maxShots;
        }

        public void SetShootingMode(Worlds.Entities.ShootingMode shootingMode)
        {
            _shootingMode = shootingMode;
        }

        public void SetMaxAngle(float maxAngle)
        {
            _maxAngle = maxAngle;
        }
#endif
    }
}
