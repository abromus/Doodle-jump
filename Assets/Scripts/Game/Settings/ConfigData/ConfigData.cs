using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(ConfigData), menuName = ConfigKeys.GamePathKey + nameof(ConfigData))]
    internal sealed class ConfigData : ScriptableObject, IConfigData
    {
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private DoodlerConfig _doodlerConfig;

        public ICameraConfig CameraConfig => _cameraConfig;

        public IDoodlerConfig DoodlerConfig => _doodlerConfig;
    }
}
