using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(GeneratorConfig), menuName = ConfigKeys.GamePathKey + nameof(GeneratorConfig))]
    internal sealed class GeneratorConfig : ScriptableObject, IGeneratorConfig
    {
        [SerializeField] private int _platformCount;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        public int PlatformCount => _platformCount;

        public float MinY => _minY;

        public float MaxY => _maxY;
    }
}
