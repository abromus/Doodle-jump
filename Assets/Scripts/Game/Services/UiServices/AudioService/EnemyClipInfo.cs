using UnityEngine;

namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct EnemyClipInfo
    {
        [SerializeField] private EnemyClipType _clipType;
        [SerializeField] private AudioClip _audioClip;

        internal readonly EnemyClipType ClipType => _clipType;

        internal readonly AudioClip AudioClip => _audioClip;
    }
}
