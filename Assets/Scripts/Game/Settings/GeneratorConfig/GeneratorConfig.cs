using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(GeneratorConfig), menuName = ConfigKeys.GamePathKey + nameof(GeneratorConfig))]
    internal sealed class GeneratorConfig : ScriptableObject, IGeneratorConfig
    {
        [SerializeField] private Vector3 _platformsStartPosition;
        [SerializeField] private Vector3 _enemiesStartPosition;
        [SerializeField] private Vector3 _boostersStartPosition;
        [SerializeField] private int _platformStartCount;
        [SerializeField] private ProgressInfo[] _progressInfos;

        public Vector3 PlatformsStartPosition => _platformsStartPosition;

        public Vector3 EnemiesStartPosition => _enemiesStartPosition;

        public Vector3 BoostersStartPosition => _boostersStartPosition;

        public int PlatformStartCount => _platformStartCount;

        public IProgressInfo[] ProgressInfos => _progressInfos;
    }
}
