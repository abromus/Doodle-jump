namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct EnemyClipInfo
    {
        [UnityEngine.SerializeField] private EnemyClipType _clipType;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _audioClip;

        internal readonly EnemyClipType ClipType => _clipType;

        internal readonly UnityEngine.AudioClip AudioClip => _audioClip;
    }
}
