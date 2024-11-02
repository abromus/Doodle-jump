namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct ProjectileClipInfo
    {
        [UnityEngine.SerializeField] private ProjectileClipType _clipType;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _audioClip;

        internal readonly ProjectileClipType ClipType => _clipType;

        internal readonly UnityEngine.AudioClip AudioClip => _audioClip;
    }
}
