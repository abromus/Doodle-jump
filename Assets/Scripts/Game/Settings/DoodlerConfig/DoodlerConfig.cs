using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(DoodlerConfig), menuName = ConfigKeys.GamePathKey + nameof(DoodlerConfig))]
    internal sealed class DoodlerConfig : ScriptableObject, IDoodlerConfig
    {
        [SerializeField] private float _movementVelocity = 7.5f;
        [SerializeField] private bool _canShootAround;

        public float MovementVelocity => _movementVelocity;

        public bool CanShootAround => _canShootAround;
    }
}
