using UnityEngine;

namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct EnemyTriggerClipInfo
    {
        [SerializeField] private EnemyTriggerClipType _clipType;
        [SerializeField] private AudioClip _audioClip;

        internal readonly EnemyTriggerClipType ClipType => _clipType;

        internal readonly AudioClip AudioClip => _audioClip;
    }
}
