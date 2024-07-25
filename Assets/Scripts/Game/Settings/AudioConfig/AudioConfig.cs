using UnityEngine;

namespace DoodleJump.Game.Settings
{
    [CreateAssetMenu(fileName = nameof(AudioConfig), menuName = ConfigKeys.GamePathKey + nameof(AudioConfig))]
    internal sealed class AudioConfig : ScriptableObject, IAudioConfig
    {
        [SerializeField] private bool _isBackgroundMusicActive;
        [SerializeField] private bool _isMusicActive;
        [SerializeField] private float _backgroundMusicVolume;
        [SerializeField] private float _soundVolume;

        public bool IsBackgroundMusicActive => _isBackgroundMusicActive;

        public bool IsSoundsActive => _isMusicActive;

        public float BackgroundMusicVolume => _backgroundMusicVolume;

        public float SoundVolume => _soundVolume;
    }
}
