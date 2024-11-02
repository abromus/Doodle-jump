namespace DoodleJump.Game.Settings
{
    [UnityEngine.CreateAssetMenu(fileName = nameof(AudioConfig), menuName = ConfigKeys.GamePathKey + nameof(AudioConfig))]
    internal sealed class AudioConfig : UnityEngine.ScriptableObject, IAudioConfig
    {
        [UnityEngine.SerializeField] private bool _isBackgroundMusicActive;
        [UnityEngine.SerializeField] private bool _isMusicActive;
        [UnityEngine.SerializeField] private float _backgroundMusicVolume;
        [UnityEngine.SerializeField] private float _soundVolume;

        public bool IsBackgroundMusicActive => _isBackgroundMusicActive;

        public bool IsSoundsActive => _isMusicActive;

        public float BackgroundMusicVolume => _backgroundMusicVolume;

        public float SoundVolume => _soundVolume;
    }
}
