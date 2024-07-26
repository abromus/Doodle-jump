using UnityEngine;

namespace DoodleJump.Core.Settings
{
    [CreateAssetMenu(fileName = nameof(InputConfig), menuName = ConfigKeys.CorePathKey + nameof(InputConfig))]
    internal sealed class InputConfig : ScriptableObject, IInputConfig
    {
        [SerializeField] [Min(0f)] private float _minXSensitivity = 0.1f;
        [SerializeField] [Min(0f)] private float _maxXSensitivity = 1f;
        [SerializeField] [Min(0f)] private float _currentXSensitivity = 0.5f;

        public float MinXSensitivity => _minXSensitivity;

        public float MaxXSensitivity => _maxXSensitivity;

        public float CurrentXSensitivity => _currentXSensitivity;
    }
}
