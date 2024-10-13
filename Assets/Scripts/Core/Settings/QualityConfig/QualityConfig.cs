using UnityEngine;

namespace DoodleJump.Core.Settings
{
    [CreateAssetMenu(fileName = nameof(QualityConfig), menuName = ConfigKeys.CorePathKey + nameof(QualityConfig))]
    internal sealed class QualityConfig : ScriptableObject, IQualityConfig
    {
        [SerializeField][Min(0f)] private bool _isVerticalSyncEnabled;
        [SerializeField][Min(0f)] private bool _isFpsUnlimited;
        [SerializeField][Min(0f)] private int _minFps = 20;
        [SerializeField][Min(0f)] private int _currentFps = 20;
        [SerializeField][Min(0f)] private int _maxFps = 250;

        public bool IsVerticalSyncEnabled => _isVerticalSyncEnabled;

        public bool IsFpsUnlimited => _isFpsUnlimited;

        public int MinFps => _minFps;

        public int CurrentFps => _currentFps;

        public int MaxFps => _maxFps;

        private void OnValidate()
        {
            _currentFps = Mathf.Clamp(_currentFps, _minFps, _maxFps);
        }
    }
}
