namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct EnemyTriggerClipInfo
    {
        [UnityEngine.SerializeField] private EnemyTriggerClipType _clipType;
        [UnityEngine.SerializeField] private UnityEngine.AudioClip _audioClip;

        internal readonly EnemyTriggerClipType ClipType => _clipType;

        internal readonly UnityEngine.AudioClip AudioClip => _audioClip;
    }
}
