using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(GeneratorConfig), menuName = ConfigKeys.GamePathKey + nameof(GeneratorConfig))]
    internal sealed class GeneratorConfig : ScriptableObject, IGeneratorConfig
    {
        [SerializeField] private Vector3 _startPosition;
        [SerializeField] private int _platformStartCount;
        [SerializeField] private ProgressInfo[] _progressInfos;

        public Vector3 StartPosition => _startPosition;

        public int PlatformStartCount => _platformStartCount;

        public IProgressInfo[] ProgressInfos => _progressInfos;
    }
}
