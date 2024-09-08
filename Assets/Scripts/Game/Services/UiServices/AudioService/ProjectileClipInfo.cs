using UnityEngine;

namespace DoodleJump.Game.Services
{
    [System.Serializable]
    internal struct ProjectileClipInfo
    {
        [SerializeField] private ProjectileClipType _clipType;
        [SerializeField] private AudioClip _audioClip;

        internal readonly ProjectileClipType ClipType => _clipType;

        internal readonly AudioClip AudioClip => _audioClip;
    }
}
