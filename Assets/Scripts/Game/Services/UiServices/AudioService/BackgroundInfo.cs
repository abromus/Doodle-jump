namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct BackgroundInfo
    {
        [UnityEngine.SerializeField] private BackgroundType _backgroundType;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _audioClip;

        internal readonly BackgroundType BackgroundType => _backgroundType;

        internal readonly UnityEngine.AudioClip AudioClip => _audioClip;
    }
}
