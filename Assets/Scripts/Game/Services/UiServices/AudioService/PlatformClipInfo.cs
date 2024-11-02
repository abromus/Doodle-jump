namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct PlatformClipInfo
    {
        [UnityEngine.SerializeField] private PlatformClipType _clipType;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _audioClip;

        internal readonly PlatformClipType ClipType => _clipType;

        internal readonly UnityEngine.AudioClip AudioClip => _audioClip;
    }
}
