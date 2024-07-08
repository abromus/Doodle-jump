using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(DoodlerConfig), menuName = ConfigKeys.GamePathKey + nameof(DoodlerConfig))]
    internal sealed class DoodlerConfig : ScriptableObject, IDoodlerConfig
    {
        [SerializeField] private float _movementVelocity = 7.5f;

        public float MovementVelocity => _movementVelocity;
    }
}
