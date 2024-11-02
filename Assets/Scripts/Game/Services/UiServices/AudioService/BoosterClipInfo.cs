namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct BoosterClipInfo
    {
        [UnityEngine.SerializeField] private BoosterClipType _clipType;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _audioClip;

        internal readonly BoosterClipType ClipType => _clipType;

        internal readonly UnityEngine.AudioClip AudioClip => _audioClip;
    }
}
