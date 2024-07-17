using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(ConfigData), menuName = ConfigKeys.GamePathKey + nameof(ConfigData))]
    internal sealed class ConfigData : ScriptableObject, IConfigData
    {
        [SerializeField] private CameraConfig _cameraConfig;
        [SerializeField] private DoodlerConfig _doodlerConfig;
        [SerializeField] private GeneratorConfig _generatorConfig;

        public ICameraConfig CameraConfig => _cameraConfig;

        public IDoodlerConfig DoodlerConfig => _doodlerConfig;

        public IGeneratorConfig GeneratorConfig => _generatorConfig;
    }
}
