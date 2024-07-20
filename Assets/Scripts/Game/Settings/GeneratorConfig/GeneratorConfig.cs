using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(GeneratorConfig), menuName = ConfigKeys.GamePathKey + nameof(GeneratorConfig))]
    internal sealed class GeneratorConfig : ScriptableObject, IGeneratorConfig
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private int _platformStartCount;
        [SerializeField] private int _platformMaxCount;
        [SerializeField] private float _minY;
        [SerializeField] private float _maxY;

        public Vector3 StartPosition => _startPosition;

        public int PlatformStartCount => _platformStartCount;

        public int PlatformMaxCount => _platformMaxCount;

        public float MinY => _minY;

        public float MaxY => _maxY;
    }
}
