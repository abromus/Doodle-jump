using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(DoodlerConfig), menuName = ConfigKeys.GamePathKey + nameof(DoodlerConfig))]

#if UNITY_EDITOR
    public sealed class DoodlerConfig : ScriptableObject, IDoodlerConfig
#else
    internal sealed class DoodlerConfig : ScriptableObject, IDoodlerConfig
#endif
    {
        [SerializeField] private float _movementVelocity = 7.5f;
        [SerializeField] private int _maxShots = 120;
        [SerializeField] private Worlds.Entities.ShootingMode _shootingMode;
        [SerializeField] private float _maxAngle;
        [SerializeField] private float _shootModeDuration = 0.2f;

        public float MovementVelocity => _movementVelocity;

        public int MaxShots => _maxShots;

        public Worlds.Entities.ShootingMode ShootingMode => _shootingMode;

        public float MaxAngle => _maxAngle;

        public float ShootModeDuration => _shootModeDuration;

#if UNITY_EDITOR
        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetMovementVelocity(float movementVelocity)
        {
            _movementVelocity = movementVelocity;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetMaxShots(int maxShots)
        {
            _maxShots = maxShots;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetShootingMode(Worlds.Entities.ShootingMode shootingMode)
        {
            _shootingMode = shootingMode;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetMaxAngle(float maxAngle)
        {
            _maxAngle = maxAngle;
        }

        [System.Runtime.CompilerServices.MethodImpl(System.Runtime.CompilerServices.MethodImplOptions.AggressiveInlining)]
        public void SetShootModeDuration(float duration)
        {
            _shootModeDuration = duration;
        }
#endif
    }
}
